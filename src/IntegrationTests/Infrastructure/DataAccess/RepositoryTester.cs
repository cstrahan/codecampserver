using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Common;
using CodeCampServer.Core.Domain.Bases;
using NBehave.Spec.NUnit;
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
			var one = CreateValidInput();
			var two = CreateValidInput();
			var three = CreateValidInput();
			PersistEntities(one, two, three);

			var repository = CreateRepository();

			var returnedFromDatabase = repository.GetById(one.Id);
			returnedFromDatabase.ShouldEqual(one);
		}

		protected virtual TRepository CreateRepository()
		{
			return GetInstance<TRepository>();
		}

		[Test]
		public virtual void Should_get_all()
		{
			var one = CreateValidInput();
			var two = CreateValidInput();
			var three = CreateValidInput();
			PersistEntities(one, two, three);

			var repository = CreateRepository();

			var all = repository.GetAll();
			all.Length.ShouldEqual(3);
		}

		[Test]
		public virtual void Should_save_one()
		{
			var one = CreateValidInput();
			var repository = CreateRepository();
			repository.Save(one);

			CommitChanges();

			using (var session = GetSession())
			{
				var reloaded = session.Load<T>(one.Id);
				reloaded.Id.ShouldEqual(one.Id);
			}
		}

		[Test]
		public virtual void Should_delete()
		{
			var one = CreateValidInput();
			var two = CreateValidInput();
			var three = CreateValidInput();

			PersistEntities(one, two, three);

			var repository = CreateRepository();

			repository.Delete(one);

			CommitChanges();

			using (var session = GetSession())
			{
				var all = session.CreateCriteria(typeof (T)).List<T>().ToArray();

				CollectionAssert.DoesNotContain(all, one);
				CollectionAssert.Contains(all, two);
				CollectionAssert.Contains(all, three);
			}
		}

		protected virtual T CreateValidInput()
		{
			return new T();
		}
		protected void CheckQueryIsLimited(Func<TRepository, T[]> query)
		{
			CheckQueryIsLimited(CreateValidInput, query);
		}
		protected void CheckQueryIsLimited(Func<T> createEntity, Func<TRepository, T[]> query)
		{
			int expectedLength = QueryLimitExtensions.ResultsLimit;
			int createdLength = QueryLimitExtensions.ResultsLimit + 1;
			CheckQueryLimit(createdLength, expectedLength, createEntity, query);
		}

		protected void CheckQueryIsNotLimited(Func<TRepository, T[]> query)
		{
			CheckQueryIsNotLimited(CreateValidInput, query);
		}

		protected void CheckQueryIsNotLimited(Func<T> createEntity, Func<TRepository, T[]> query)
		{
			int expectedLength = QueryLimitExtensions.ResultsLimit + 1;
			int createdLength = QueryLimitExtensions.ResultsLimit + 1;
			CheckQueryLimit(createdLength, expectedLength, createEntity, query);
		}

		private void CheckQueryLimit(int createdLength, int expectedLength, Func<T> createEntity, Func<TRepository, T[]> query)
		{
			var entities = new List<PersistentObject>();
			for (int i = 0; i < createdLength; i++)
			{
				entities.Add(createEntity());
			}
			PersistEntities(entities.ToArray());

			var results = query(CreateRepository());
			results.Length.ShouldEqual(expectedLength);
		}
	}
}