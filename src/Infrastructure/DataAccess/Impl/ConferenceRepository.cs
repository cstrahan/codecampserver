using System;
using System.Linq;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class ConferenceRepository : KeyedRepository<Conference>, IConferenceRepository
	{
		public ConferenceRepository(ISessionBuilder sessionFactory) : base(sessionFactory) {}

		public Conference GetNextConference()
		{
			return GetSession().CreateQuery(
				"from Conference conf where conf.StartDate >= :today order by conf.StartDate").SetDateTime(
				"today", DateTime.Now.Midnight()).SetMaxResults(1).UniqueResult<Conference>();
		}

		public Conference[] GetAllForUserGroup(UserGroup usergroup)
		{
			return GetSession().CreateQuery(
				"from Conference conf where conf.UserGroup = :usergroup order by conf.StartDate desc").
				SetEntity("usergroup",
				          usergroup).List<Conference>().ToArray();
		}

		public Conference[] GetFutureForUserGroup(UserGroup usergroup)
		{
			return GetSession().CreateQuery(
				"from Conference conf where conf.UserGroup = :usergroup and conf.EndDate >= :datetime order by conf.StartDate")
				.SetEntity("usergroup", usergroup)
				.SetDateTime("datetime", SystemTime.Now().Midnight())
				.List<Conference>().ToArray();
		}
	}
}