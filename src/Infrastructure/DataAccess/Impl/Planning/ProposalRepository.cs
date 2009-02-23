using CodeCampServer.Core.Domain.Model.Planning;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl.Planning
{
	public class ProposalRepository : RepositoryBase<Proposal>, IProposalRepository
	{
		public ProposalRepository(ISessionBuilder sessionFactory) : base(sessionFactory) {}
	}
}