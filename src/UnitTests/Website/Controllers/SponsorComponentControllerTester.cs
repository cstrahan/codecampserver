using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class SponsorComponentControllerTester
    {
        private MockRepository _mocks;

        [Test]
        public void ListShouldRenderListView()
        {
            var sponsors = new[] {new Sponsor(), new Sponsor()};
            _mocks = new MockRepository();
            var repository = _mocks.DynamicMock<IConferenceRepository>();
            var conference = _mocks.DynamicMock<Conference>();
            SetupResult.For(repository.GetConferenceByKey("austincodecamp2008")).Return(conference);
            SetupResult.For(conference.GetSponsors(SponsorLevel.Platinum)).Return(sponsors);
            _mocks.ReplayAll();

            var sponsorComponentController = new TestSponsorComponentController(repository);
            sponsorComponentController.List("austincodecamp2008", SponsorLevel.Platinum);
            
            Assert.That(sponsorComponentController.ActualViewName, Is.EqualTo("List"));
        }

        [Test]
        public void ListShouldFillViewDataWithSponsors()
        {
            var sponsors = new[] { new Sponsor(), new Sponsor() };
            _mocks = new MockRepository();
            var repository = _mocks.DynamicMock<IConferenceRepository>();
            var conference = _mocks.DynamicMock<Conference>();
            SetupResult.For(repository.GetConferenceByKey("austincodecamp2008")).Return(conference);
            SetupResult.For(conference.GetSponsors(SponsorLevel.Platinum)).Return(sponsors);
            _mocks.ReplayAll();

            var sponsorComponentController = new TestSponsorComponentController(repository);
            sponsorComponentController.List("austincodecamp2008", SponsorLevel.Platinum);

            Assert.That(sponsorComponentController.ActualViewData, Is.TypeOf(typeof(Sponsor[])));
        }

        private class TestSponsorComponentController : SponsorComponentController
        {
            public object ActualViewData;
            public string ActualViewName;

            public TestSponsorComponentController(IConferenceRepository _conferenceRepository)
                : base(_conferenceRepository)
            {
            }

            public override void RenderView(string actualViewName)
            {
                ActualViewName = actualViewName;
            }

            public override void RenderView(string actualViewName, object actualViewData)
            {
                ActualViewData = actualViewData;
                ActualViewName = actualViewName;
            }
        }
    }
}