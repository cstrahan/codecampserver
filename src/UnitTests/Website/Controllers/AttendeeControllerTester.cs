using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using MvcContrib;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class when_listing_attendees : behaves_like_mock_test
    {
        private AttendeesController _controller;
        private IUserSession _userSession;
        private IConferenceRepository _conferenceRepository;
        private ActionResult _result;

        public override void Setup()
        {
            base.Setup();

            _userSession = Mock<IUserSession>();
            _conferenceRepository = Mock<IConferenceRepository>();
            _controller = new AttendeesController(_userSession, _conferenceRepository);

            var conference = new Conference("houstonTechFest", "Houston Tech Fest");
            _conferenceRepository.Expect(x => x.GetConferenceByKey("houstonTechFest")).Return(conference);

            _result = _controller.List("houstonTechFest");            
        }

        [Test]
        public void should_get_conference_by_key()
        {
            _conferenceRepository.VerifyAllExpectations();
        }

        [Test]
        public void should_render_default_view()
        {            
            _result.ShouldRenderDefaultView();            
        }

        [Test]
        public void attendees_should_be_added_to_view_data()
        {
            var viewResult = (ViewResult) _result;
            Assert.That(viewResult.ViewData.Get<Person[]>(), Is.Not.Null);
        }
    }
}