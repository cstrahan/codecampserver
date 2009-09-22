using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class SpeakerRepository : KeyedRepository<Speaker>, ISpeakerRepository
	{
		public SpeakerRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}

		protected override string GetEntityNaturalKeyName()
		{
			return KEY_NAME;
		}

	    public Speaker[] GetAllForConference(Conference conference)
	    {
            Speaker[] list =
                GetSession().CreateQuery("from Speaker s where s.Conference = :conference").SetEntity("conference", conference).List
                    <Speaker>().ToArray();

            return list;
        }
	}
}