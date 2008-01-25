using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class SponsorControllerTester
    {
        private MockRepository _mocks;
		private IConferenceService _service;
		private Conference _conference;

	
        [Test]
        public void ShouldListSponsorsForAConference()
        {
            _mocks = new MockRepository();
            _service = _mocks.CreateMock<IConferenceService>();
            _conference = _mocks.CreateMock<Conference>();

            ConfirmedSponsor[] toReturn = new ConfirmedSponsor[]
                {
                    new ConfirmedSponsor(new Sponsor("name", "logourl", "website"), SponsorLevel.Platinum),
                    new ConfirmedSponsor(new Sponsor("name2", "logourl2", "website2"), SponsorLevel.Bronze) 
                };
            
            SetupResult.For(_service.GetConference("austincodecamp2008")).Return(_conference);
            SetupResult.For(_conference.GetSponsors()).Return(toReturn);
            _mocks.ReplayAll();
           
            TestingSponsorController controller = new TestingSponsorController(_service, new ClockStub());          
            controller.List("austincodecamp2008");
            Assert.That(controller.ActualViewName, Is.EqualTo("List"));
            
            SmartBag viewData = controller.ActualViewData as SmartBag;
            Assert.That(viewData, Is.Not.Null);
            Assert.That(viewData.Contains<ConfirmedSponsor[]>());
            
            ConfirmedSponsor[] sponsors = viewData.Get<ConfirmedSponsor[]>();
            Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
            Assert.That(sponsors[1].Level, Is.EqualTo(SponsorLevel.Bronze));
            Assert.That(sponsors[0].Sponsor.Name, Is.EqualTo("name"));
            Assert.That(sponsors[1].Sponsor.Name, Is.EqualTo("name2"));
        }


        private class TestingSponsorController : SponsorController
        {
            public string ActualViewName;
            public string ActualMasterName;
            public object ActualViewData;

            public TestingSponsorController(IConferenceService conferenceService, IClock clock)
                : base(conferenceService, clock)
            {
            }

            protected override void RenderView(string viewName,
                                               string masterName,
                                               object viewData)
            {
                ActualViewName = viewName;
                ActualMasterName = masterName;
                ActualViewData = viewData;
            }
        }
    }
}
