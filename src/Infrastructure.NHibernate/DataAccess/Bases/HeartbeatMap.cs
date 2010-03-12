using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases
{
	public class HeartbeatMap : EntityClassMap<Heartbeat>
	{
		public HeartbeatMap()
		{
			Map(x => x.Message);
			Map(x => x.Date);
		}
	}
}