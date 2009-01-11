using System;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IConferenceMessage
	{
		string Key { get; set; }
		Guid Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
		string StartDate { get; set; }
		string EndDate { get; set; }
		string LocationName { get; set; }
		string Address { get; set; }
		string City { get; set; }
		string Region { get; set; }
		string PostalCode { get; set; }
		string PhoneNumber { get; set; }
	}
}