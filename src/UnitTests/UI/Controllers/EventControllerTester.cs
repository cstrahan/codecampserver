using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using NUnit.Framework;
using Rhino.Mocks;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	[TestFixture]
	public class EventControllerTester : TestBase
	{
		[Test]
		public void Should_render_conference_announcement_for_conference()
		{
			var mapper = S<IConferenceMapper>();
			var conference = new Conference();
			var form = new ConferenceInput();
			mapper.Stub(s => s.Map(conference)).Return(form);

			var controller = new EventController(null, mapper, null);
			ViewResult result = controller.Announcement(conference);
			result.ViewName.ShouldEqual("Conference" + EventController.ANNOUNCEMENT_PARTIAL_SUFFIX);
			result.ViewData.Model.ShouldEqual(form);
		}

		[Test]
		public void Should_render_meeting_announcement_for_meeting()
		{
			var mapper = S<IMeetingMapper>();
			var meeting = new Meeting();
			var form = new MeetingInput();
			mapper.Stub(s => s.Map(meeting)).Return(form);

			var controller = new EventController(null, null, mapper);
			ViewResult result = controller.Announcement(meeting);
			result.ViewName.ShouldEqual("Meeting" + EventController.ANNOUNCEMENT_PARTIAL_SUFFIX);
			result.ViewData.Model.ShouldEqual(form);
		}

		[Test]
		public void Should_list_upcoming_events_for_usergroup()
		{
			var repository = S<IEventRepository>();
			var usergroup = new UserGroup();
			var meeting = new Meeting(){Key = "meeting1"};
			var conference = new Conference(){Key = "conference1"};
			repository.Stub(s => s.GetFutureForUserGroup(usergroup)).Return(new Event[] {meeting, conference});

			var controller = new EventController(repository, null, null);
			ViewResult result = controller.UpComing(usergroup);
			result.ViewName.ShouldEqual("list");
			result.ViewData.Model.ShouldEqual(new []{"meeting1", "conference1"});
		}

		[Test]
		public void Should_list_all_events_for_usergroup()
		{
			var repository = S<IEventRepository>();
			var usergroup = new UserGroup();
			var meeting = new Meeting() { Key = "meeting1" };
			var conference = new Conference() { Key = "conference1" };
			repository.Stub(s => s.GetAllForUserGroup(usergroup)).Return(new Event[] { meeting, conference });

			var controller = new EventController(repository, null, null);
			ViewResult result = controller.List(usergroup);
			result.ViewName.ShouldEqual("list");
			result.ViewData.Model.ShouldEqual(new[] { "meeting1", "conference1" });
		}

        [Test]
        public void Should_list_all_future_events()
        {
            var repository = S<IEventRepository>();
            var usergroup = new UserGroup();
            var userGroup1 = new UserGroup(){Name = "foo",DomainName = "bar"};
            var meeting = new Meeting() { Key = "meeting1" ,Name = "monthly meeting", Topic = "Visual Studio Tips and Tricks",UserGroup = userGroup1};
            var conference = new Conference() { Key = "conference1" , Name = "Austin Code Camp",UserGroup = userGroup1};
            repository.Stub(s => s.GetAllFutureEvents()).Return(new Event[] { meeting, conference });

            var controller = new EventController(repository, null, null);
            ViewResult result = controller.AllUpcomingEvents();
            result.ViewName.ShouldEqual("");
            result.ViewData.Model.ShouldBeInstanceOf<EventList[]>();        }
    }
}