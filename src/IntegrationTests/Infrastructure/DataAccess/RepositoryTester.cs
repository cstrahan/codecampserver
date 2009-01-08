using CodeCampServer.Core.Domain;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using StructureMap;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public abstract class RepositoryTester<T, TRepository> : DataTestBase where T : PersistentObject, new()
	                                                                      where TRepository : IRepository<T>
	{
		[Test]
		public virtual void Should_get_by_id()
		{
			var one = new T();
			var two = new T();
			var three = new T();
			PersistEntities(one, two, three);

			var repository = ObjectFactory.GetInstance<TRepository>();

			T returnedFromDatabase = repository.GetById(one.Id);
			returnedFromDatabase.ShouldEqual(one);
		}

		[Test]
		public virtual void Should_get_all()
		{
			var one = new T();
			var two = new T();
			var three = new T();
			PersistEntities(one, two, three);

			var repository = ObjectFactory.GetInstance<TRepository>();

			T[] all = repository.GetAll();
			all.Length.ShouldEqual(3);
		}

		[Test]
		public virtual void Should_save_one()
		{
			var one = new T();
			var repository = ObjectFactory.GetInstance<TRepository>();
			repository.Save(one);

			GetSession().Dispose();

			using (ISession session = GetSession())
			{
				var reloaded = session.Load<T>(one.Id);
				reloaded.Id.ShouldEqual(one.Id);
			}
		}
	}
}