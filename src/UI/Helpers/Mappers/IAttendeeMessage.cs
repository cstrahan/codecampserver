using System;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IAttendeeMessage
	{
		string FirstName { get; set; }
		string LastName { get; set; }
		string EmailAddress { get; set; }
		AttendanceStatus Status { get; set; }
		string Webpage { get; set; }
		Guid ConferenceID { get; set; }
		Guid? AttendeeID { get; set; }
	}
}