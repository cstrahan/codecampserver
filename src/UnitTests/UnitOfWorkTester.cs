using System;
using System.Collections;
using System.Data;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using NBehave.Spec.NUnit;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Stat;
using NHibernate.Type;
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
			var session = S<ISession>();
			var sessionSource = new SessionBuilderStub(session);

			session.Stub(s => s.Transaction).Return(S<ITransaction>());
			session.Stub(s => s.BeginTransaction()).Return(S<ITransaction>());

			var uow = new UnitOfWork(sessionSource);

			uow.Begin();

			uow.GetSession().ShouldEqual(session);
		}

		[Test]
		public void Should_start_a_new_transaction_when_begun()
		{
			var session = S<ISession>();
			var sessionSource = new SessionBuilderStub(session);
			var transaction = S<ITransaction>();

			session.Stub(s => s.Transaction).Return(transaction);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			var uow = new UnitOfWork(sessionSource);

			uow.Begin();

			session.AssertWasCalled(s => s.BeginTransaction());
		}

		[Test]
		public void Should_commit_the_begun_transaction_when_committing()
		{
			var session = S<ISession>();
			var sessionSource = new SessionBuilderStub(session);
			var transaction = S<ITransaction>();

			session.Stub(s => s.Transaction).Return(transaction);
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
			var session = S<ISession>();
			var sessionSource = new SessionBuilderStub(session);
			var transaction = S<ITransaction>();

			session.Stub(s => s.Transaction).Return(transaction);
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
			var session = S<ISession>();
			var sessionSource = new SessionBuilderStub(session);
			var transaction = S<ITransaction>();

			session.Stub(s => s.Transaction).Return(transaction);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			var uow = new UnitOfWork(sessionSource);

			typeof (InvalidOperationException).ShouldBeThrownBy(uow.Commit);
		}

		[Test]
		public void Should_dispose_transaction_and_session_when_disposing()
		{
			var session = S<ISession>();
			var sessionSource = new SessionBuilderStub(session);
			var transaction = S<ITransaction>();

			session.Stub(s => s.Transaction).Return(transaction);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			var uow = new UnitOfWork(sessionSource);

			uow.Begin();
			uow.Dispose();

			session.AssertWasCalled(s => s.Dispose());
		}

		[Test]
		public void Should_dispose_when_not_begun()
		{
			var session = S<ISession>();
			var uow = new UnitOfWork(new SessionBuilderStub(session));

			typeof (Exception).ShouldNotBeThrownBy(uow.Dispose);
			session.AssertWasCalled(x=>x.Dispose());
		}

		[Test]
		public void Should_dispose_twice_without_problems()
		{
			var session = S<ISession>();
			var sessionSource = new SessionBuilderStub(session);
			var transaction = S<ITransaction>();

			session.Stub(s => s.Transaction).Return(transaction);
			session.Stub(s => s.BeginTransaction()).Return(transaction);
			var uow = new UnitOfWork(sessionSource);

			uow.Begin();
			uow.Dispose();

			typeof (Exception).ShouldNotBeThrownBy(uow.Dispose);
		}
	}

	public class SessionStub : ISession {
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void Flush()
		{
			throw new NotImplementedException();
		}

		public IDbConnection Disconnect()
		{
			throw new NotImplementedException();
		}

		public void Reconnect()
		{
			throw new NotImplementedException();
		}

		public void Reconnect(IDbConnection connection)
		{
			throw new NotImplementedException();
		}

		public IDbConnection Close()
		{
			throw new NotImplementedException();
		}

		public void CancelQuery()
		{
			throw new NotImplementedException();
		}

		public bool IsDirty()
		{
			throw new NotImplementedException();
		}

		public object GetIdentifier(object obj)
		{
			throw new NotImplementedException();
		}

		public bool Contains(object obj)
		{
			throw new NotImplementedException();
		}

		public void Evict(object obj)
		{
			throw new NotImplementedException();
		}

		public object Load(Type theType, object id, LockMode lockMode)
		{
			throw new NotImplementedException();
		}

		public object Load(string entityName, object id, LockMode lockMode)
		{
			throw new NotImplementedException();
		}

		public object Load(Type theType, object id)
		{
			throw new NotImplementedException();
		}

		public T Load<T>(object id, LockMode lockMode)
		{
			throw new NotImplementedException();
		}

		public T Load<T>(object id)
		{
			throw new NotImplementedException();
		}

		public object Load(string entityName, object id)
		{
			throw new NotImplementedException();
		}

		public void Load(object obj, object id)
		{
			throw new NotImplementedException();
		}

		public void Replicate(object obj, ReplicationMode replicationMode)
		{
			throw new NotImplementedException();
		}

		public void Replicate(string entityName, object obj, ReplicationMode replicationMode)
		{
			throw new NotImplementedException();
		}

		public object Save(object obj)
		{
			throw new NotImplementedException();
		}

		public void Save(object obj, object id)
		{
			throw new NotImplementedException();
		}

		public object Save(string entityName, object obj)
		{
			throw new NotImplementedException();
		}

		public void SaveOrUpdate(object obj)
		{
			throw new NotImplementedException();
		}

		public void SaveOrUpdate(string entityName, object obj)
		{
			throw new NotImplementedException();
		}

		public void Update(object obj)
		{
			throw new NotImplementedException();
		}

		public void Update(object obj, object id)
		{
			throw new NotImplementedException();
		}

		public void Update(string entityName, object obj)
		{
			throw new NotImplementedException();
		}

		public object Merge(object obj)
		{
			throw new NotImplementedException();
		}

		public object Merge(string entityName, object obj)
		{
			throw new NotImplementedException();
		}

		public void Persist(object obj)
		{
			throw new NotImplementedException();
		}

		public void Persist(string entityName, object obj)
		{
			throw new NotImplementedException();
		}

		public object SaveOrUpdateCopy(object obj)
		{
			throw new NotImplementedException();
		}

		public object SaveOrUpdateCopy(object obj, object id)
		{
			throw new NotImplementedException();
		}

		public void Delete(object obj)
		{
			throw new NotImplementedException();
		}

		public void Delete(string entityName, object obj)
		{
			throw new NotImplementedException();
		}

		public IList Find(string query)
		{
			throw new NotImplementedException();
		}

		public IList Find(string query, object value, IType type)
		{
			throw new NotImplementedException();
		}

		public IList Find(string query, object[] values, IType[] types)
		{
			throw new NotImplementedException();
		}

		public IEnumerable Enumerable(string query)
		{
			throw new NotImplementedException();
		}

		public IEnumerable Enumerable(string query, object value, IType type)
		{
			throw new NotImplementedException();
		}

		public IEnumerable Enumerable(string query, object[] values, IType[] types)
		{
			throw new NotImplementedException();
		}

		public ICollection Filter(object collection, string filter)
		{
			throw new NotImplementedException();
		}

		public ICollection Filter(object collection, string filter, object value, IType type)
		{
			throw new NotImplementedException();
		}

		public ICollection Filter(object collection, string filter, object[] values, IType[] types)
		{
			throw new NotImplementedException();
		}

		public int Delete(string query)
		{
			throw new NotImplementedException();
		}

		public int Delete(string query, object value, IType type)
		{
			throw new NotImplementedException();
		}

		public int Delete(string query, object[] values, IType[] types)
		{
			throw new NotImplementedException();
		}

		public void Lock(object obj, LockMode lockMode)
		{
			throw new NotImplementedException();
		}

		public void Lock(string entityName, object obj, LockMode lockMode)
		{
			throw new NotImplementedException();
		}

		public void Refresh(object obj)
		{
			throw new NotImplementedException();
		}

		public void Refresh(object obj, LockMode lockMode)
		{
			throw new NotImplementedException();
		}

		public LockMode GetCurrentLockMode(object obj)
		{
			throw new NotImplementedException();
		}

		public ITransaction BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public ITransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			throw new NotImplementedException();
		}

		public ICriteria CreateCriteria<T>() where T : class
		{
			throw new NotImplementedException();
		}

		public ICriteria CreateCriteria<T>(string alias) where T : class
		{
			throw new NotImplementedException();
		}

		public ICriteria CreateCriteria(Type persistentClass)
		{
			throw new NotImplementedException();
		}

		public ICriteria CreateCriteria(Type persistentClass, string alias)
		{
			throw new NotImplementedException();
		}

		public ICriteria CreateCriteria(string entityName)
		{
			throw new NotImplementedException();
		}

		public ICriteria CreateCriteria(string entityName, string alias)
		{
			throw new NotImplementedException();
		}

		public IQuery CreateQuery(string queryString)
		{
			throw new NotImplementedException();
		}

		public IQuery CreateFilter(object collection, string queryString)
		{
			throw new NotImplementedException();
		}

		public IQuery GetNamedQuery(string queryName)
		{
			throw new NotImplementedException();
		}

		public IQuery CreateSQLQuery(string sql, string returnAlias, Type returnClass)
		{
			throw new NotImplementedException();
		}

		public IQuery CreateSQLQuery(string sql, string[] returnAliases, Type[] returnClasses)
		{
			throw new NotImplementedException();
		}

		public ISQLQuery CreateSQLQuery(string queryString)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public object Get(Type clazz, object id)
		{
			throw new NotImplementedException();
		}

		public object Get(Type clazz, object id, LockMode lockMode)
		{
			throw new NotImplementedException();
		}

		public object Get(string entityName, object id)
		{
			throw new NotImplementedException();
		}

		public T Get<T>(object id)
		{
			throw new NotImplementedException();
		}

		public T Get<T>(object id, LockMode lockMode)
		{
			throw new NotImplementedException();
		}

		public string GetEntityName(object obj)
		{
			throw new NotImplementedException();
		}

		public IFilter EnableFilter(string filterName)
		{
			throw new NotImplementedException();
		}

		public IFilter GetEnabledFilter(string filterName)
		{
			throw new NotImplementedException();
		}

		public void DisableFilter(string filterName)
		{
			throw new NotImplementedException();
		}

		public IMultiQuery CreateMultiQuery()
		{
			throw new NotImplementedException();
		}

		public ISession SetBatchSize(int batchSize)
		{
			throw new NotImplementedException();
		}

		public ISessionImplementor GetSessionImplementation()
		{
			throw new NotImplementedException();
		}

		public IMultiCriteria CreateMultiCriteria()
		{
			throw new NotImplementedException();
		}

		public ISession GetSession(EntityMode entityMode)
		{
			throw new NotImplementedException();
		}

		public EntityMode ActiveEntityMode
		{
			get { throw new NotImplementedException(); }
		}

		public FlushMode FlushMode
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public CacheMode CacheMode
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public ISessionFactory SessionFactory
		{
			get { throw new NotImplementedException(); }
		}

		public IDbConnection Connection
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsOpen
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsConnected
		{
			get { throw new NotImplementedException(); }
		}

		public ITransaction Transaction
		{
			get { throw new NotImplementedException(); }
		}

		public ISessionStatistics Statistics
		{
			get { throw new NotImplementedException(); }
		}
	}

	public class SessionBuilderStub : ISessionBuilder {
		private ISession _session;
		public SessionBuilderStub(ISession session)
		{
			_session = session;
		}

		public ISession GetSession()
		{
			return _session;
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