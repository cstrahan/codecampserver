using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Services.Unique
{
	public interface IEntitySpecification<TModel> where TModel : PersistentObject
	{
		Expression<Func<TModel, object>> PropertyExpression { get;}
		object Value { get; }
		object ExistingId { get; }
		bool HasExistingId { get; }
	}
}