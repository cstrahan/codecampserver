using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Services.Unique;

namespace CodeCampServer.UnitTests.Core.Domain
{
	public class EntityCounterSpy<TModel> : IEntityCounter where TModel : PersistentObject
	{
		private int _count;

		public static EntityCounterSpy<TModel> With()
		{
			return new EntityCounterSpy<TModel>();
		}

		public EntityCounterSpy<TModel> StubbedCount(int count)
		{
			_count = count;
			return this;
		}

		public IEntitySpecification<TModel> Specification { get; private set; }

		public int CountByProperty<T>(IEntitySpecification<T> specification) where T : PersistentObject
		{
			Specification = (IEntitySpecification<TModel>) specification;
			return _count;
		}
	}
}