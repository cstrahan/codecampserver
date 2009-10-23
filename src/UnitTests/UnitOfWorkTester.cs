using System;
using CodeCampServer.Infrastructure.DataAccess;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests
{
	[TestFixture]
	public class UnitOfWorkTester : TestBase
	{
		[Test]
		public void Should_populate_current_session_when_begun()
		{
			var sessionSource = S<ISessionBuilder>();
			var session = S<ISession>();
			
			sessionSource.Stub(s => s.GetSession()).Return(session);
			session.Stub(s => s.BeginTransaction()).Return(S<ITransaction>());

			var uow = new UnitOfWork(sessionSource);

			uow.Begin();

			uow.CurrentSession.ShouldEqual(session);
		}

		[Test]
		public void Should_start_a_new_transaction_when_begun()
		{
			var sessionSource = S<ISessionBuilder>();
			var session = S<ISession>();
			var transaction = S<ITransaction>();

			sessionSource.Stub(s => s.GetSession()).Return(session);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			var uow = new UnitOfWork(sessionSource);

			uow.Begin();

			session.AssertWasCalled(s => s.BeginTransaction());
		}

		[Test]
		public void Should_commit_the_begun_transaction_when_committing()
		{
			var sessionSource = S<ISessionBuilder>();
			var session = S<ISession>();
			var transaction = S<ITransaction>();


			sessionSource.Stub(s => s.GetSession()).Return(session);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			transaction.Stub(t => t.IsActive).Return(true);
			var uow = new UnitOfWork(sessionSource);

			uow.Begin();
			uow.Commit();

			transaction.AssertWasCalled(t => t.Commit());
		}

		[Test]
		public void Should_rollback_the_transaction_when_rolling_back()
		{
			var sessionSource = S<ISessionBuilder>();
			var session = S<ISession>();
			var transaction = S<ITransaction>();

			sessionSource.Stub(s => s.GetSession()).Return(session);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			transaction.Stub(t => t.IsActive).Return(true);
			var uow = new UnitOfWork(sessionSource);

			uow.Begin();
			uow.RollBack();

			transaction.AssertWasCalled(t => t.Rollback());
		}

		[Test]
		public void Should_require_the_uow_to_have_begun_if_committing()
		{
			var sessionSource = S<ISessionBuilder>();
			var session = S<ISession>();
			var transaction = S<ITransaction>();

			sessionSource.Stub(s => s.GetSession()).Return(session);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			var uow = new UnitOfWork(sessionSource);

			typeof(InvalidOperationException).ShouldBeThrownBy(uow.Commit);
		}

		[Test]
		public void Should_require_the_uow_to_have_begun_if_rolling_back()
		{
			var sessionSource = S<ISessionBuilder>();
			var session = S<ISession>();
			var transaction = S<ITransaction>();

			sessionSource.Stub(s => s.GetSession()).Return(session);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			var uow = new UnitOfWork(sessionSource);

			typeof(InvalidOperationException).ShouldBeThrownBy(uow.RollBack);
		}

		[Test]
		public void Should_not_commit_the_transaction_if_rolled_back()
		{
			var sessionSource = S<ISessionBuilder>();
			var session = S<ISession>();
			var transaction = S<ITransaction>();

			sessionSource.Stub(s => s.GetSession()).Return(session);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			transaction.Stub(t => t.IsActive).Return(true);
			var uow = new UnitOfWork(sessionSource);

			uow.Begin();
			uow.RollBack();

			uow.Commit();
			transaction.AssertWasNotCalled(t => t.Commit());
		}

		[Test]
		public void Should_dispose_transaction_and_session_when_disposing()
		{
			var sessionSource = S<ISessionBuilder>();
			var session = S<ISession>();
			var transaction = S<ITransaction>();

			sessionSource.Stub(s => s.GetSession()).Return(session);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			var uow = new UnitOfWork(sessionSource);

			uow.Begin();
			uow.Dispose();

			transaction.AssertWasCalled(t => t.Dispose());
			session.AssertWasCalled(s => s.Dispose());
		}

		[Test]
		public void Should_dispose_when_not_begun()
		{
			var uow = new UnitOfWork(null);

			typeof(Exception).ShouldNotBeThrownBy(uow.Dispose);
		}

		[Test]
		public void Should_dispose_twice_without_problems()
		{
			var sessionSource = S<ISessionBuilder>();
			var session = S<ISession>();
			var transaction = S<ITransaction>();

			sessionSource.Stub(s => s.GetSession()).Return(session);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			var uow = new UnitOfWork(sessionSource);

			uow.Begin();
			uow.Dispose();

			typeof(Exception).ShouldNotBeThrownBy(uow.Dispose);
		}

	}

	public static class TestExtensions
	{
		public static void ShouldNotBeThrownBy(this Type exceptionType, Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				ex.ShouldNotBeInstanceOfType(exceptionType);
			}
		}
	
	}
}