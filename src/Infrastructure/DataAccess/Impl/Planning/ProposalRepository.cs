using System.Linq;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Planning;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl.Planning
{
	public class ProposalRepository : RepositoryBase<Proposal>, IProposalRepository
	{
		public ProposalRepository(ISessionBuilder sessionFactory) : base(sessionFactory) {}

		public Proposal[] GetByConference(Conference conference)
		{
			return GetSession()
				.CreateQuery("from Proposal p where p.Conference = ? order by p.CreatedDate desc")
				.SetEntity(0, conference).List<Proposal>().ToArray();
		}
	}
}