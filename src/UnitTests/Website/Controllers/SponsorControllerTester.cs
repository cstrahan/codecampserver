using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
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
		[SetUp]
		public void Setup()
		{
			_userSession = MockRepository.GenerateMock<IUserSession>();
			_conferenceRepository = MockRepository.GenerateStub<IConferenceRepository>();
			_conference = new Conference("austincodecamp2008", "Austin Code Camp");
		}

		private Conference _conference;
		private IUserSession _userSession;
		private IConferenceRepository _conferenceRepository;

		private SponsorController getController()
		{
			return new SponsorController(_conferenceRepository, _userSession);
		}
        
		[Test]
		public void DeleteShouldRemoveSponsorAndRedirectToList()
		{
			var sponsorToDelete = new Sponsor("delete", "logourl", "website", "", "", "", SponsorLevel.Platinum);
			var sponsor = new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum);
			_conference.AddSponsor(sponsor);
			_conference.AddSponsor(sponsorToDelete);
			_conferenceRepository.Stub(r => r.GetConferenceByKey("austincodecamp2008")).Return(_conference);

			SponsorController controller = getController();
		    var actionResult = controller.Delete(_conference.Key, "delete") as RedirectToRouteResult;

			_conferenceRepository.AssertWasCalled(r => r.Save(_conference));

		    var sponsors = new List<Sponsor>(_conference.GetSponsors());
			
            Assert.That(sponsors.Contains(sponsorToDelete), Is.False);
			Assert.That(sponsors.Contains(sponsor));
			
            Assert.That(actionResult, Is.Not.Null);			
            Assert.That(actionResult.Values["action"], Is.EqualTo("list"));
            Assert.That(actionResult.Values["conferenceKey"], Is.EqualTo(_conference.Key));
		}

		[Test]
		public void EditSponsorShouldBeMarkedAsAdminOnly()
		{
			MethodInfo method = typeof (SponsorController).GetMethod("Edit");
			Assert.That(method.HasAdminOnlyAttribute());
		}

		[Test]
		public void EditSponsorShouldGetSponsorData()
		{
			_conference.AddSponsor(new Sponsor("test", "", "", "", "", "", SponsorLevel.Gold));
			_conferenceRepository.Stub(r => r.GetConferenceByKey("austincodecamp2008")).Return(_conference);

			SponsorController controller = getController();
			var actionResult = controller.Edit("austincodecamp2008", "test") as ViewResult;

			var viewDataSponsor = controller.ViewData.Get<Sponsor>();
			Assert.That(viewDataSponsor, Is.Not.Null);
			Assert.That(viewDataSponsor.Name, Is.EqualTo("test"));
			Assert.That(actionResult.ViewName, Is.Null);
		}

		[Test]
		public void EditSponsorShouldRedirectToListWhenNoSponsor()
		{
			_conferenceRepository.Stub(r => r.GetConferenceByKey("austincodecamp2008")).Return(_conference);

			SponsorController controller = getController();
			var actionResult = controller.Edit("austincodecamp2008", null) as RedirectToRouteResult;

			Assert.That(actionResult, Is.Not.Null, "Should have returned action");
			Assert.That(actionResult.RedirectsToAction("list"));
		}

		[Test]
		public void NewActionShouldRenderEditViewWithNewSponsor()
		{
			SponsorController controller = getController();
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

			_conferenceRepository.Stub(r => r.GetConferenceByKey("austincodecamp2008")).Return(_conference);
			_userSession.Expect(s => s.PushUserMessage(FlashMessage.MessageType.Message, "The sponsor was saved"));

			SponsorController controller = getController();
			var actionResult = controller.Save(_conference.Key, "name", "edited name", "Gold", "", "", "", "", "")
			                   as RedirectToRouteResult;

			_conferenceRepository.AssertWasCalled(r => r.Save(_conference));

			//check redirect
			Assert.That(actionResult, Is.Not.Null, "Should have returned RedirectToRouteResult");
			Assert.That(actionResult.RedirectsToAction("list"));

			_userSession.VerifyAllExpectations();
		}

		[Test]
		public void ShouldListSponsorsForAConference()
		{
			_conference.AddSponsor(new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum));
			_conference.AddSponsor(new Sponsor("name2", "logourl2", "website2", "", "", "", SponsorLevel.Bronze));
			_conferenceRepository.Stub(r => r.GetConferenceByKey("austincodecamp2008")).Return(_conference);

			SponsorController controller = getController();
			var actionResult = controller.List("austincodecamp2008", null, null) as ViewResult;

			Assert.That(actionResult, Is.Not.Null, "should have returned ViewResult");
			Assert.That(actionResult.ViewName, Is.Null);

			var sponsors = controller.ViewData.Model as Sponsor[];
			Assert.That(sponsors, Is.Not.Null);
			Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
			Assert.That(sponsors[1].Level, Is.EqualTo(SponsorLevel.Bronze));
			Assert.That(sponsors[0].Name, Is.EqualTo("name"));
			Assert.That(sponsors[1].Name, Is.EqualTo("name2"));
		}

		[Test]
		public void ShouldListSponsorsByLevelForAConferenceWithPartial()
		{
			_conference.AddSponsor(new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum));
			_conference.AddSponsor(new Sponsor("name2", "logourl2", "website2", "", "", "", SponsorLevel.Bronze));
			_conferenceRepository.Stub(r => r.GetConferenceByKey("austincodecamp2008")).Return(_conference);

			SponsorController controller = getController();
			var actionResult = controller.List("austincodecamp2008", true, SponsorLevel.Platinum) as ViewResult;

			Assert.That(actionResult, Is.Not.Null, "should have returned ViewResult");
			Assert.That(actionResult.ViewName, Is.EqualTo("SponsorList"));

			var sponsors = controller.ViewData.Model as Sponsor[];
			Assert.That(sponsors, Is.Not.Null);
			Assert.That(sponsors.Length, Is.EqualTo(1));
			Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
			Assert.That(sponsors[0].Name, Is.EqualTo("name"));
		}
	}
}