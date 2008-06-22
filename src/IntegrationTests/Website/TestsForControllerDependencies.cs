using CodeCampServer.Website;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Helpers;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Website
{
	[TestFixture]
	public class TestsForControllerDependencies
	{
		[Test]
		public void CanCreateConferenceController()
		{
			Global.RegisterMvcTypes();
			IoC.Resolve<AdminController>();
			IoC.Resolve<ConferenceController>();
			IoC.Resolve<LoginController>();
			IoC.Resolve<ScheduleController>();
			IoC.Resolve<SessionController>();
			IoC.Resolve<SpeakerController>();
			IoC.Resolve<SponsorComponentController>();
			IoC.Resolve<SponsorController>();
			IoC.Resolve<TrackController>();
		}
	}
}