using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using MvcContrib;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class SponsorComponentControllerTester
	{
		private IConferenceRepository _repository;
		private IUserSession _session;

		[SetUp]
		public void SetUp()
		{
			_repository = MockRepository.GenerateStub<IConferenceRepository>();
			_session = MockRepository.GenerateMock<IUserSession>();
		}

		[Test]
        public void ListShouldRenderListView()
		{
			var sponsors = new[] {new Sponsor(), new Sponsor()};
			var conference = MockRepository.GenerateStub<Conference>();
            
			_repository.Stub(r => r.GetConferenceByKey("austincodecamp2008")).Return(conference);
			conference.Stub(c => c.GetSponsors(SponsorLevel.Platinum)).Return(sponsors);

			var sponsorComponentController = new SponsorComponentController(_repository, _session);

			var result = sponsorComponentController.List("austincodecamp2008", SponsorLevel.Platinum);

			result.ViewName.ShouldEqual("SponsorList");
		}

		[Test]
		public void ListShouldFillViewDataWithSponsors()
		{
			var sponsors = new[] {new Sponsor(), new Sponsor()};
			var conference = MockRepository.GenerateStub<Conference>();

			_repository.Stub(r => r.GetConferenceByKey("austincodecamp2008")).Return(conference);
			conference.Stub(c => c.GetSponsors(SponsorLevel.Platinum)).Return(sponsors);

			var sponsorComponentController = new SponsorComponentController(_repository, _session);
			var result = sponsorComponentController.List("austincodecamp2008", SponsorLevel.Platinum);

			result.ViewData.Model.ShouldNotBeNull();
			(result.ViewData.Model as Sponsor[]).ShouldNotBeEmpty();
		}
	}
}