using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Website.Controllers;
using MvcContrib;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class SpeakerControllerTester
	{		
		private IConferenceRepository _conferenceRepository;
		private IUserSession _userSession;
		private Conference _conference;

		[SetUp]
		public void Setup()
		{			
			_conferenceRepository = MockRepository.GenerateMock<IConferenceRepository>();
            _userSession = MockRepository.GenerateMock<IUserSession>();
			_conference = new Conference("austincodecamp2008", "Austin Code Camp");
		}

		private SpeakerController createSpeakerController()
		{
			return new SpeakerController(_conferenceRepository, _userSession, new ClockStub());
		}

		[Test]
		public void ShouldViewSpeakerDetailsByDisplayName()
		{
			Speaker speaker = getSpeaker();

            _conferenceRepository.Stub(x => x.GetConferenceByKey("austincodecamp2008")).Return(_conference);
			
            _conference.AddSpeaker(speaker.Person, speaker.SpeakerKey, speaker.Bio, speaker.AvatarUrl);
			

			SpeakerController controller = createSpeakerController();
			var result = controller.Details("austincodecamp2008", speaker.SpeakerKey) as ViewResult;
			var viewDataSpeakerProfile = controller.ViewData.Get<Speaker>();

			Assert.That(viewDataSpeakerProfile, Is.Not.Null);
			Assert.That(viewDataSpeakerProfile, Is.EqualTo(speaker));

            if(result == null)
                Assert.Fail("Expected ViewResult");

			Assert.That(result.ViewName, Is.EqualTo("view"));
		}

		[Test]
		public void EditSpeakerShouldGetSpeakerData()
		{
			Speaker speaker = getSpeaker();
			_conference.AddSpeaker(speaker.Person, speaker.SpeakerKey, speaker.Bio, speaker.AvatarUrl);

		    _userSession.Stub(x => x.GetLoggedInPerson()).Return(speaker.Person);
			_conferenceRepository.Stub(x => x.GetConferenceByKey(null)).IgnoreArguments().Return(_conference);
			

			var controller = createSpeakerController();
			controller.Edit("conf123").ShouldRenderDefaultView();
			
			var viewDataSpeakerProfile = controller.ViewData.Get<Speaker>();
			Assert.That(speaker, Is.EqualTo(viewDataSpeakerProfile));
		}

		[Test]
		public void EditProfileShouldReturnLoginWhenNoSpeaker()
		{
			_conferenceRepository.Stub(x=>x.GetConferenceByKey(null)).IgnoreArguments().Return(_conference);
			_userSession.Stub(x=>x.GetLoggedInPerson()).Return(null);

			var controller = createSpeakerController();
		    controller.Edit("conf123").ShouldRedirectTo("login", "index");
		}

		[Test]
		public void list_action_should_fetch_speakers_and_render_default_view()
		{
			var p = new Person("joe", "dimaggio", "jd@baseball.com");
			var p2 = new Person("marilyn", "monroe", "m@m.com");

			_conference.AddSpeaker(p, "joedimaggio", "bio here...", "avatar.jpg");
			_conference.AddSpeaker(p2, "marilynmonroe", "bio here...", "avatar.jpg");
			
		    _conferenceRepository.Stub(x => x.GetConferenceByKey("austinCodeCamp2008")).Return(_conference);

            var controller = createSpeakerController();
			controller.List("austinCodeCamp2008").ShouldRenderDefaultView();
			
			var speakersPassedtoView = controller.ViewData.Get<Speaker[]>();
			Assert.That(speakersPassedtoView, Is.Not.Null);
			Assert.That(speakersPassedtoView.Length, Is.EqualTo(2));
		}

		private Speaker getSpeaker()
		{
			var person = new Person("joe", "blow", "jb@aol.com");
			return new Speaker(person, "jb", "bio", "avatar.jpg");
		}
	}
}