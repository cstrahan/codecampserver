using System.Collections.Generic;
using System.Reflection;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Controllers;
using MvcContrib;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class SponsorControllerTester
    {
        private const string CONFERENCE_KEY = "austincodecamp2008";

        [SetUp]
        public void Setup()
        {
            _userSession = MockRepository.GenerateMock<IUserSession>();
            _conferenceRepository = MockRepository.GenerateStub<IConferenceRepository>();
            _conference = new Conference(CONFERENCE_KEY, "Austin Code Camp");

            _conferenceRepository.Stub(r => r.GetConferenceByKey(CONFERENCE_KEY)).Return(_conference);
        }

        private Conference _conference;
        private IUserSession _userSession;
        private IConferenceRepository _conferenceRepository;

        private SponsorController getSponsorController()
        {
            return new SponsorController(_conferenceRepository, _userSession);
        }

        [Test]
        public void DeleteShouldRemoveSponsorAndRedirectToList()
        {
            var sponsorToDelete = new Sponsor("deleteme", "logourl", "website", "", "", "", SponsorLevel.Platinum);
            var sponsor = new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum);
            _conference.AddSponsor(sponsor);
            _conference.AddSponsor(sponsorToDelete);            

            var controller = getSponsorController();
            controller.Delete(_conference.Key, "deleteme").ShouldRedirectTo("list")
                .WithValue("conferenceKey", CONFERENCE_KEY);

            _conferenceRepository.AssertWasCalled(r => r.Save(_conference));

            var sponsors = new List<Sponsor>(_conference.GetSponsors());

            Assert.That(sponsors.Contains(sponsorToDelete), Is.False);
            Assert.That(sponsors.Contains(sponsor));            
        }

        [Test]
        public void EditSponsorShouldBeMarkedAsAdminOnly()
        {
            MethodInfo method = typeof (SponsorController).GetMethod("Edit");
            Assert.That(method.HasAdminAuthorizationAttribute());
        }

        [Test]
        public void edit_action_should_load_sponsor_and_render_default_view()
        {
            _conference.AddSponsor(new Sponsor("test", "", "", "", "", "", SponsorLevel.Gold));
            
            var controller = getSponsorController();
            controller.Edit(CONFERENCE_KEY, "test").ShouldRenderDefaultView();

            controller.ViewData.Contains<Sponsor>().ShouldBeTrue();
            controller.ViewData.Get<Sponsor>().Name.ShouldEqual("test");
        }

        [Test]
        public void EditSponsorShouldRedirectToListWhenNoSponsor()
        {            
            var controller = getSponsorController();
            controller.Edit(CONFERENCE_KEY, null).ShouldRedirectTo("list");                        
        }

        [Test]
        public void NewActionShouldRenderEditViewWithNewSponsor()
        {
            var controller = getSponsorController();
            controller.New(_conference.Key).ShouldRenderView("edit");
            controller.ViewData.Contains<Sponsor>().ShouldBeTrue();            
        }

        [Test]
        public void SaveShouldProperlyEditExistingSponsor()
        {
            _conference.AddSponsor(new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum));
            _conference.AddSponsor(new Sponsor("name2", "logourl2", "website2", "", "", "", SponsorLevel.Bronze));

            _userSession.Expect(s => s.PushUserMessage(FlashMessage.MessageType.Message, "The sponsor was saved"));

            var controller = getSponsorController();
            controller.Save(_conference.Key, "name", "edited name", "Gold", "", "", "", "", "")
                .ShouldRedirectTo("list");                              

            _conferenceRepository.AssertWasCalled(r => r.Save(_conference));            
            _userSession.VerifyAllExpectations();
        }

        [Test]
        public void ShouldListSponsorsForAConference()
        {
            _conference.AddSponsor(new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum));
            _conference.AddSponsor(new Sponsor("name2", "logourl2", "website2", "", "", "", SponsorLevel.Bronze));

            var controller = getSponsorController();
            controller.List(CONFERENCE_KEY, null, null).ShouldRenderDefaultView();
            
            var sponsors = controller.ViewData.Model as Sponsor[];
            if(sponsors == null)
                Assert.Fail("should have set a model");

            Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
            Assert.That(sponsors[1].Level, Is.EqualTo(SponsorLevel.Bronze));
            Assert.That(sponsors[0].Name, Is.EqualTo("name"));
            Assert.That(sponsors[1].Name, Is.EqualTo("name2"));
        }
        
        [Test]
        public void ShouldListZeroSponsorsOnEmptyRepository()
        {
            var controller = getSponsorController();
            
            controller.List(CONFERENCE_KEY, null, null).ShouldRenderDefaultView();
            
            var sponsors = controller.ViewData.Model as Sponsor[];
            Assert.That(sponsors, Is.Not.Null, "should have returned sponsors array");
            Assert.That(sponsors.Length, Is.EqualTo(0), "should have returned zero-length sponsors array");
        }

        [Test]
        public void ShouldListSponsorsByLevelForAConferenceWithPartial()
        {
            _conference.AddSponsor(new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum));
            _conference.AddSponsor(new Sponsor("name2", "logourl2", "website2", "", "", "", SponsorLevel.Bronze));

            var controller = getSponsorController();
            controller.List(CONFERENCE_KEY, true, SponsorLevel.Platinum).ShouldRenderView("SponsorList");
            
            var sponsors = controller.ViewData.Model as Sponsor[];
            Assert.That(sponsors, Is.Not.Null);
            Assert.That(sponsors.Length, Is.EqualTo(1));
            Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
            Assert.That(sponsors[0].Name, Is.EqualTo("name"));
        }
    }
}
