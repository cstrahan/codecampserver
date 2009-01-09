using CodeCampServer.Core.Domain;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public abstract class RepositoryTester<T, TRepository> : DataTestBase
		where T : PersistentObject, new()
		where TRepository : IRepository<T>
	{
		[Test]
		public virtual void Should_get_by_id()
		{
			var one = new T();
			var two = new T();
			var three = new T();
			PersistEntities(one, two, three);

			var repository = CreateRepository();

			T returnedFromDatabase = repository.GetById(one.Id);
			returnedFromDatabase.ShouldEqual(one);
		}

		protected abstract TRepository CreateRepository();

		[Test]
		public virtual void Should_get_all()
		{
			var one = new T();
			var two = new T();
			var three = new T();
			PersistEntities(one, two, three);

			var repository = CreateRepository();

			T[] all = repository.GetAll();
			all.Length.ShouldEqual(3);
		}

		[Test]
		public virtual void Should_save_one()
		{
			var one = new T();
			var repository = CreateRepository();
			repository.Save(one);

			GetSession().Dispose();

			using (ISession session = GetSession())
			{
				var reloaded = session.Load<T>(one.Id);
				reloaded.Id.ShouldEqual(one.Id);
			}
		}

		[Test]
		public virtual void Should_delete()
		{
			var one = new T();
			var two = new T();
			var three = new T();
			PersistEntities(one, two, three);

			var repository = CreateRepository();

			repository.Delete(one);
			
			GetSession().Dispose();

			T[] all = repository.GetAll();
			CollectionAssert.DoesNotContain(all, one);
			CollectionAssert.Contains(all, two);
			CollectionAssert.Contains(all, three);
		}
	}
}