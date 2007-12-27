using System;
using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model
{
    [PluginFamily("Default")]
    public interface IConferenceRepository
    {
        IEnumerable<Conference> GetAllConferences();
        Conference GetConferenceByKey(string key);
        Conference GetFirstConferenceAfterDate(DateTime date);
        Conference GetById(Guid id);
        void Save(Conference conference);
    }
}