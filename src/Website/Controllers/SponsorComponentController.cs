using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Controllers
{
	public class SponsorComponentController : ComponentControllerBase
	{
		private readonly IConferenceRepository _conferenceRepository;

		public SponsorComponentController(IConferenceRepository conferenceRepository)
		{
			_conferenceRepository = conferenceRepository;
		}

		public void List(string key, SponsorLevel level)
		{
			Conference conference = _conferenceRepository.GetConferenceByKey(key);
			Sponsor[] sponsors = conference.GetSponsors(level);
			RenderView("List", sponsors);
		}
	}
}