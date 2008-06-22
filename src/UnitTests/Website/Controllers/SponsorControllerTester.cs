using System;
using System.Collections.Generic;
using System.Web.Mvc;
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
        private IConferenceRepository _conferenceRepository;
        private Conference _conference;
        private IUserSession userSession;
        private TempDataDictionary _tempData;


        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _conferenceRepository = _mocks.CreateMock<IConferenceRepository>();
            userSession = _mocks.CreateMock<IUserSession>();
            _conference = new Conference("austincodecamp2008", "Austin Code Camp");
            _tempData = new TempDataDictionary(_mocks.FakeHttpContext("~/sponsors"));
        }

        [Test]
        public void DeleteShouldRemoveSponsorAndRenderList()
        {
            var sponsorToDelete = new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum);
            var sponsor = new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum);
            _conference.AddSponsor(sponsor);
            _conference.AddSponsor(sponsorToDelete);
            
            
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            Expect.Call(() => _conferenceRepository.Save(_conference));            
            _mocks.ReplayAll();


            var controller = GetController();

            var actionResult = controller.Delete(_conference.Key, "name") as ViewResult;

            Assert.That(controller.ViewData.Contains<Sponsor[]>());
            
            var sponsors = new List<Sponsor>(controller.ViewData.Get<Sponsor[]>());
            Assert.That(sponsors.Contains(sponsorToDelete), Is.False);

            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.ViewName, Is.EqualTo("List"));
        }

        [Test]
        public void EditSponsorShouldGetSponsorData()
        {
            _conference.AddSponsor(new Sponsor("test", "", "", "", "", "", SponsorLevel.Gold));
            SetupResult.For(userSession.IsAdministrator).Return(true);
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);

            _mocks.ReplayAll();

            var controller = GetController();
            var actionResult = controller.Edit("austincodecamp2008", "test") as ViewResult;

            var viewDataSponsor = controller.ViewData.Get<Sponsor>();
            Assert.That(viewDataSponsor, Is.Not.Null);
            Assert.That(viewDataSponsor.Name, Is.EqualTo("test"));
            Assert.That(actionResult.ViewName, Is.Null);
        }

        [Test]
        public void EditSponsorShouldRedirectToListWhenNoSponsor()
        {
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            SetupResult.For(userSession.IsAdministrator).Return(true);
            _mocks.ReplayAll();

            var controller = GetController();
            var actionResult = controller.Edit("austincodecamp2008", null) as RedirectToRouteResult;

            Assert.That(actionResult, Is.Not.Null, "Should have returned action");            
            Assert.That(actionResult.Values["action"].ToString().ToLower(), Is.EqualTo("list"));
        }

        [Test]
        public void EditSponsorShouldBeMarkedAsAdminOnly()
        {            
            var method = typeof (SponsorController).GetMethod("Edit");
            Assert.That(method.HasAdminOnlyAttribute());
        }

        [Test]
        public void NewActionShouldRenderEditViewWithNewSponsor()
        {
            var controller = GetController();
            var actionResult = controller.New(_conference.Key) as ViewResult;

            Assert.That(controller.ViewData.Contains<Sponsor>());
            Assert.That(actionResult, Is.Not.Null, "should have returned ViewResult");
            Assert.That(actionResult.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void SaveShouldProperlyEditExistingSponsor()
        {
            _conference.AddSponsor(new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum));
            _conference.AddSponsor(new Sponsor("name2", "logourl2", "website2", "", "", "", SponsorLevel.Bronze));

            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            Expect.Call(() => _conferenceRepository.Save(_conference)).Repeat.Twice();

            _mocks.ReplayAll();

            var controller = GetController();
            var actionResult = controller.Save(_conference.Key, "name", "edited name", "Gold", "", "", "", "", "")
                as RedirectToRouteResult;

            //check redirect
            Assert.That(actionResult, Is.Not.Null, "Should have returned RedirectToRouteResult");
            Assert.That(actionResult.Values["action"], Is.EqualTo("list"));

            //check success message
            Assert.That(controller.TempData.ContainsKey("message"));

            _mocks.VerifyAll();
        }
        
        [Test]
        public void ShouldListSponsorsForAConference()
        {
            _conference.AddSponsor(new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum));
            _conference.AddSponsor(new Sponsor("name2", "logourl2", "website2", "", "", "", SponsorLevel.Bronze));

            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);

            _mocks.ReplayAll();

            var controller = GetController();
            var actionResult = controller.List("austincodecamp2008") as ViewResult;

            Assert.That(actionResult, Is.Not.Null, "should have returned ViewResult");
            Assert.That(actionResult.ViewName, Is.Null);

            Assert.That(controller.ViewData, Is.Not.Null);
            Assert.That(controller.ViewData.Contains<Sponsor[]>());

            var sponsors = controller.ViewData.Get<Sponsor[]>();
            Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
            Assert.That(sponsors[1].Level, Is.EqualTo(SponsorLevel.Bronze));
            Assert.That(sponsors[0].Name, Is.EqualTo("name"));
            Assert.That(sponsors[1].Name, Is.EqualTo("name2"));
        }

        private SponsorController GetController()
        {
            return new SponsorController(_conferenceRepository, userSession)
                       {
                           TempData = _tempData
                       };
        }
    }
}