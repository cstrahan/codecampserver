using System.Linq;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
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

			TRepository repository = CreateRepository();

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

			TRepository repository = CreateRepository();

			T[] all = repository.GetAll();
			all.Length.ShouldEqual(3);
		}

		[Test]
		public virtual void Should_save_one()
		{
			var one = new T();
			TRepository repository = CreateRepository();
			repository.Save(one);

			CommitChanges();

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

			TRepository repository = CreateRepository();

			repository.Delete(one);

			CommitChanges();

			using (var session = GetSession())
			{
				T[] all = session.CreateCriteria(typeof(T)).List<T>().ToArray();

				CollectionAssert.DoesNotContain(all, one);
				CollectionAssert.Contains(all, two);
				CollectionAssert.Contains(all, three);
			}
		}
	}
}