using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Common;
using CodeCampServer.Core.Services;

namespace CodeCampServer.UnitTests.Core.Domain
{
	public class EntityCounterSpy : IEntityCounter
	{
		private int _count;

		public static EntityCounterSpy With() { return new EntityCounterSpy(); }

		public EntityCounterSpy StubbedCount(int count)
		{
			_count = count;
			return this;
		}

		public int CountByProperty<TModel>(Expression<Func<TModel, object>> propertyExpression, object value) where TModel : PersistentObject
		{
			PropertyName = UINameHelper.BuildNameFrom(propertyExpression);
			Value = value;
			return _count;
		}

		public string PropertyName { get; private set; }
		public object Value { get; private set; }
	}
}