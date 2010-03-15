using CodeCampServer.Core.Common;
using NHibernate;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Common
{
	public static class QueryExtensions
	{
		public static ICriteria Limit(this ICriteria criteria)
		{
			return criteria.SetMaxResults(QueryLimitExtensions.ResultsLimit);
		}
	}
}