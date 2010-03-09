using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl
{
	public class SponsorRepository : RepositoryBase<Sponsor>, ISponsorRepository
	{}
}