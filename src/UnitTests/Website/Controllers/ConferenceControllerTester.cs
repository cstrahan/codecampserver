using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class ConferenceControllerTester
    {
        [Test]
        public void ScheduleShouldGetConferenceByKeyAndSendScheduleToTheView()
        {
            MockRepository mocks = new MockRepository();
            IConferenceRepository repository =
                mocks.CreateMock<IConferenceRepository>();
            Conference conference = new Conference("austincodecamp2008",
                                                   "Austin Code Camp");
            SetupResult.For(repository.GetConferenceByKey("austincodecamp2008"))
                .Return(conference);
            mocks.ReplayAll();

            TestingConferenceController controller =
                new TestingConferenceController(repository);
            controller.Schedule("austincodecamp2008");

            Assert.That(controller.ActualViewName, Is.EqualTo("showschedule"));
            Schedule actualViewData = controller.ActualViewData as Schedule;
            Assert.That(actualViewData, Is.Not.Null);
            Assert.That(actualViewData.ConferenceName,
                        Is.EqualTo("Austin Code Camp"));
        }

        private class TestingConferenceController : ConferenceController
        {
            public string ActualViewName;
            public string ActualMasterName;
            public object ActualViewData;

            public TestingConferenceController(
                IConferenceRepository conferenceRepository)
                : base(conferenceRepository)
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