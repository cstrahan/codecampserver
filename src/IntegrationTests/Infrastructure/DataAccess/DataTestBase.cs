using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public abstract class DataTestBase : IntegrationTestBase
	{
		[SetUp]
		public override void Setup()
		{
			DeleteAllObjects();
			base.Setup();
		}

		protected void PersistEntities(params PersistentObject[] entities)
		{
			using (ISession session = GetSession())
			{
				Persist(entities, session);
			}
		}

		protected void UsingSession(Action<ISession> doStuffInTheSession)
		{
			using (ISession session = GetSession())
			{
				doStuffInTheSession(session);
			}
		}

		protected virtual void DeleteAllObjects()
		{
			var unit = new UnitOfWork(GetSessionSource());
			unit.Begin();
			new DatabaseDeleter(unit).DeleteAllObjects();
			unit.Dispose();
		}


		protected static void Persist(IEnumerable<PersistentObject> entities, ISession session)
		{
			foreach (var entity in entities)
			{
				session.SaveOrUpdate(entity);
			}
			session.Flush();
		}

		protected void PersistEntity(PersistentObject entity)
		{
			PersistEntities(entity);
		}

		protected void AssertObjectCanBePersisted<T>(T persistentObject) where T : PersistentObject
		{
			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(persistentObject);
				session.Flush();
			}

			using (ISession session = GetSession())
			{
				var reloadedObject = session.Load<T>(persistentObject.Id);
				Assert.That(reloadedObject, Is.EqualTo(persistentObject));
				Assert.That(reloadedObject, Is.Not.SameAs(persistentObject));
				AssertObjectsMatch(persistentObject, reloadedObject);
			}
		}

		protected void Reload<TEntity>(ref TEntity entity)
			where TEntity : PersistentObject
		{
			ISession session = GetSession();
			if (session.Contains(entity))
				session.Evict(entity);
			entity = session.Get<TEntity>(entity.Id);
		}
	}
}