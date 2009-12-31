using CodeCampServer.Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Mappings
{
	public class AuditedEntityClassMap<TEntity> : ClassMap<TEntity>
		where TEntity : AuditedPersistentObjectOfGuid
	{
		public AuditedEntityClassMap()
		{
			Cache.ReadWrite();
			DynamicUpdate();
			ApplyId();
			this.ChangeAuditInfo();
		}

		protected virtual void ApplyId()
		{
			this.StandardId();
		}
	}
}