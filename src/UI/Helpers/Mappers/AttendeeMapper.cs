using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class AttendeeMapper : FormMapper<Conference, AttendeeForm>, IAttendeeMapper
	{
		public AttendeeMapper(IConferenceRepository repository) : base(repository)
		{
		}

		protected override Guid GetIdFromMessage(AttendeeForm form)
		{
			return form.ConferenceID;
		}

		protected override void MapToModel(AttendeeForm form, Conference model)
		{
			Attendee attendee;

			if (FormRepresentsAnAlreadyAttendingAttendee(form, model))
			{
				attendee = model.GetAttendee(form.AttendeeID.Value);
			}
			else
			{
				attendee = new Attendee();
				model.AddAttendee(attendee);
			}

			attendee.EmailAddress = form.EmailAddress;
			attendee.FirstName = form.FirstName;
			attendee.LastName = form.LastName;
			attendee.Status = form.Status;
			attendee.Webpage = form.Webpage;

		}

		private static bool FormRepresentsAnAlreadyAttendingAttendee(AttendeeForm form, Conference conference)
		{
			return form.AttendeeID.HasValue && conference.IsAttending(form.AttendeeID.Value);
		}
	}
}