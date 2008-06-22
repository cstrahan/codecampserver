using System;

namespace CodeCampServer.Model.Domain
{
	public interface IConferenceRepository
	{
		Conference[] GetAllConferences();
		Conference GetConferenceByKey(string key);
		Conference GetFirstConferenceAfterDate(DateTime date);
		Conference GetMostRecentConference(DateTime date);
		Conference GetById(Guid id);
		void Save(Conference conference);
		bool ConferenceExists(string name, string key);
		bool ConferenceKeyAvailable(string key);
	}
}