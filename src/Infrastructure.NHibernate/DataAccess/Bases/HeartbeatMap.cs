using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases
{
	public class HeartbeatMap : AuditedEntityClassMap<Heartbeat>
	{
		public HeartbeatMap()
		{
			Map(x => x.Message);
		}
	}
}