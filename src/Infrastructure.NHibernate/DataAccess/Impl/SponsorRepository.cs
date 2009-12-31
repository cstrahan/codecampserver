using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl
{
	public class SponsorRepository : RepositoryBase<Sponsor>, ISponsorRepository
	{
		public SponsorRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {}
	}
}