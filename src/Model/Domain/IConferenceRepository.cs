using System;
using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model.Domain
{
	[PluginFamily(Keys.DEFAULT)]
	public interface IConferenceRepository
	{
		IEnumerable<Conference> GetAllConferences();
		Conference GetConferenceByKey(string key);
		Conference GetFirstConferenceAfterDate(DateTime date);
		Conference GetById(Guid id);
		void Save(Conference conference);
	}
}