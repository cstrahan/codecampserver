using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Services.Unique
{
	public class EntitySpecificationOfGuid<TModel> : IEntitySpecification<TModel> where TModel : PersistentObject, IPersistentObjectOfGuid
	{
		public Expression<Func<TModel, object>> PropertyExpression { get; set; }

		public object Value { get; set; }
		public object ExistingId { get { return Id; } }

		public Guid Id { get; set; }

		public bool HasExistingId
		{
			get { return Id != AuditedPersistentObjectOfGuid.EmptyId; }
		}
	}
}