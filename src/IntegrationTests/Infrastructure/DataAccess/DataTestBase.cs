using System;
using System.Collections.Generic;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.NHibernate;
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

		public class UserSessionDataStub : UserSessionStub
		{
			private readonly object _id;

			public UserSessionDataStub(object id) : base(null)
			{
				_id = id;
			}

			public override User GetCurrentUser()
			{
				return new SessionBuilder().GetSession().Load<User>(_id);
			}
		}

		protected void PersistEntities(params PersistentObject[] entities)
		{
			using (ISession session = GetSession())
			{
				Persist(entities, session);
			}
		}

		protected virtual void DeleteAllObjects()
		{
			new DatabaseDeleter(new SessionBuilder()).DeleteAllObjects();
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