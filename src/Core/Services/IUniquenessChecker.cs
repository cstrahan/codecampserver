using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Services
{
	public interface IUniquenessChecker
	{
		bool IsUnique<TModel>(object value, Expression<Func<TModel, object>> propertyExpression)
			where TModel : PersistentObject;

		string BuildFailureMessage<TModel>(object value, Expression<Func<TModel, object>> propertyExpression)
			where TModel : PersistentObject;
	}
}