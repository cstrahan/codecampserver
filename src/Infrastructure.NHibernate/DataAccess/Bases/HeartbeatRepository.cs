using CodeCampServer.Core.Domain.Bases;
using NHibernate.Criterion;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases
{
	public class HeartbeatRepository : RepositoryBase<Heartbeat>, IHeartbeatRepository
	{
		public Heartbeat GetLatest()
		{
			return GetSession()
				.CreateCriteria<Heartbeat>()
				.AddOrder(Order.Desc("Date"))
				.SetMaxResults(1)
				.UniqueResult<Heartbeat>();
		}
	}
}