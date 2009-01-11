using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class TimeSlotMapperTester : TestBase
	{
		[Test]
		public void Should_map_a_new_timeslot_from_form()
		{
			var form = S<TimeSlotForm>();
			form.StartTime = new DateTime(2009, 5, 30, 9, 0, 0).ToString();
			form.EndTime = new DateTime(2009, 5, 30, 10, 30, 0).ToString();
			form.ConferenceId = Guid.NewGuid();
			form.Id = Guid.Empty;
			var conference = new Conference();

			var repository = M<ITimeSlotRepository>();
			repository.Stub(x => x.GetById(form.Id)).Return(null);

			var conferenceRepository = M<IConferenceRepository>();
			conferenceRepository.Stub(x => x.GetById(form.ConferenceId)).Return(conference);

			ITimeSlotMapper mapper = new TimeSlotMapper(repository, conferenceRepository);

			TimeSlot mapped = mapper.Map(form);

			mapped.StartTime.ShouldEqual(new DateTime(2009, 5, 30, 9, 0, 0));
			mapped.EndTime.ShouldEqual(new DateTime(2009, 5, 30, 10, 30, 0));
			mapped.Conference.ShouldEqual(conference);
		}

		[Test]
		public void Should_map_an_existing_timeslot_from_form()
		{
			var form = S<TimeSlotForm>();
			form.StartTime = new DateTime(2009, 5, 30, 9, 0, 0).ToString();
			form.EndTime = new DateTime(2009, 5, 30, 10, 30, 0).ToString();
			form.ConferenceId = Guid.NewGuid();
			form.Id = Guid.Empty;
			var conference = new Conference();
			var slot = new TimeSlot();
			var repository = M<ITimeSlotRepository>();
			repository.Stub(x => x.GetById(form.Id)).Return(slot);

			var conferenceRepository = M<IConferenceRepository>();
			conferenceRepository.Stub(x => x.GetById(form.ConferenceId)).Return(conference);

			ITimeSlotMapper mapper = new TimeSlotMapper(repository, conferenceRepository);

			TimeSlot mapped = mapper.Map(form);
			slot.ShouldEqual(mapped);
			mapped.StartTime.ShouldEqual(new DateTime(2009, 5, 30, 9, 0, 0));
			mapped.EndTime.ShouldEqual(new DateTime(2009, 5, 30, 10, 30, 0));
			mapped.Conference.ShouldEqual(conference);
		}
	}
}