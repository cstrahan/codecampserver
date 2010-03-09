using CodeCampServer.Core.Bases;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases
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