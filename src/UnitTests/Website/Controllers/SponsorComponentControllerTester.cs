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
		private IConferenceRepository _repository;

		[SetUp]
		public void SetUp()
		{
			_mocks = new MockRepository();
		}

		[Test]
		public void ListShouldRenderListView()
		{
			using (_mocks.Record())
			{
				var sponsors = new[] {new Sponsor(), new Sponsor()};
				_repository = _mocks.DynamicMock<IConferenceRepository>();
				var conference = _mocks.DynamicMock<Conference>();
				SetupResult.For(_repository.GetConferenceByKey("austincodecamp2008")).Return(conference);
				SetupResult.For(conference.GetSponsors(SponsorLevel.Platinum)).Return(sponsors);
			}

			using (_mocks.Playback())
			{
				var sponsorComponentController = new TestSponsorComponentController(_repository);

				sponsorComponentController.List("austincodecamp2008", SponsorLevel.Platinum);
				Assert.That(sponsorComponentController.ActualViewName, Is.EqualTo("List"));
			}
		}

		[Test]
		public void ListShouldFillViewDataWithSponsors()
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

			Assert.That(sponsorComponentController.ActualViewData, Is.TypeOf(typeof (Sponsor[])));
		}
	}

	internal class TestSponsorComponentController : SponsorComponentController
	{
		public TestSponsorComponentController(IConferenceRepository repository) : base(repository)
		{
		}

		public override void RenderView(string viewName, object ViewData)
		{
			ActualViewName = viewName;
			ActualViewData = ViewData;
		}

		public object ActualViewData { get; set; }
		public string ActualViewName { get; set; }
	}
}