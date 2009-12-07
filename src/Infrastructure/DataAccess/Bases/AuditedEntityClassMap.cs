using CodeCampServer.Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.DataAccess.Mappings
{
	public class AuditedEntityClassMap<TEntity> : ClassMap<TEntity>
		where TEntity : AuditedPersistentObject
	{
		public AuditedEntityClassMap()
		{
			Cache.ReadWrite();
			DynamicUpdate();
			this.StandardId();
			this.ChangeAuditInfo();
		}
	}
}