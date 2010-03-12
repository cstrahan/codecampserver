using CodeCampServer.Core.Bases;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases
{
	public class EntityClassMap<TEntity> : ClassMap<TEntity>
		where TEntity : PersistentObjectOfGuid
	{
		public EntityClassMap()
		{
			Cache.ReadWrite();
			DynamicUpdate();
			ApplyId();
		}

		protected virtual void ApplyId()
		{
			this.StandardId();
		}
	}
}