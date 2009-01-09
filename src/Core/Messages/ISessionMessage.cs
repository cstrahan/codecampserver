using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;

namespace CodeCampServer.Core.Messages
{
	public interface ISessionMessage : IFormMessage
	{
		Guid Id { get; set; }
		string Key { get; set; }
		Track Track { get; set; }
		TimeSlot TimeSlot { get; set; }
		Speaker Speaker { get; set; }
		Conference Conference { get; set; }
		string RoomNumber { get; set; }
		string Title { get; set; }
		string Abstract { get; set; }
		SessionLevel Level { get; set; }
		string MaterialsUrl { get; set; }
	}
}