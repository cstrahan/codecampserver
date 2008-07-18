using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using MvcContrib;

namespace CodeCampServer.Website.Controllers
{
	public class SponsorComponentController : Controller
	{
		private readonly IConferenceRepository _conferenceRepository;

		public SponsorComponentController(IConferenceRepository conferenceRepository, IUserSession session) : base(session)
		{
			_conferenceRepository = conferenceRepository;
		}

		public ViewResult List(string key, SponsorLevel level)
		{
			Conference conference = _conferenceRepository.GetConferenceByKey(key);
			Sponsor[] sponsors = conference.GetSponsors(level);
			return View("SponsorList", sponsors);
		}
	}
}