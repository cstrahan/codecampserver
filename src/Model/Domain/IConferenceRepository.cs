using System;
using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Domain
{
	public interface IConferenceRepository
	{
		IEnumerable<Conference> GetAllConferences();
		Conference GetConferenceByKey(string key);
		Conference GetFirstConferenceAfterDate(DateTime date);
	    Conference GetMostRecentConference(DateTime date);
		Conference GetById(Guid id);
		void Save(Conference conference);
	}
}