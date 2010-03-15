using System.Linq;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Common;
using NHibernate;
using NHibernate.Criterion;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases
{
	public class HeartbeatRepository : RepositoryBase<Heartbeat>, IHeartbeatRepository
	{
		public Heartbeat GetLatest()
		{
			var criteria = GetSession().CreateCriteria<Heartbeat>();
			MakeOrdered(criteria);
			criteria.SetMaxResults(1);
			return criteria.UniqueResult<Heartbeat>();
		}

		private void MakeOrdered(ICriteria criteria)
		{
			criteria.AddOrder(Order.Desc("Date"));
		}

		public Heartbeat[] GetTop()
		{
			var criteria = GetSession().CreateCriteria<Heartbeat>();
			MakeOrdered(criteria);
			criteria.Limit();
			return criteria.List<Heartbeat>().ToArray();
		}
	}
}