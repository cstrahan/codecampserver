using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Services
{
	public interface IEntityCounter
	{
		int CountByProperty<TModel>(Expression<Func<TModel, object>> propertyExpression, object value)
			where TModel : PersistentObject;
	}
}