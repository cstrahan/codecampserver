using CodeCampServer.Core.Domain;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class SessionRepository : RepositoryBase, ISessionRepository
	{
		public SessionRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}
	}
}