using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Services.Unique
{
	public interface IUniquenessChecker
	{
		bool IsUnique<TModel>(IEntitySpecification<TModel> specification)
			where TModel : PersistentObject;

		string BuildFailureMessage<TModel>(object value, Expression<Func<TModel, object>> propertyExpression)
			where TModel : PersistentObject;
	}
}