using System;
using System.Collections.Generic;
using CodeCampServer.Domain.Model;
using StructureMap;

namespace CodeCampServer.Domain
{
    [PluginFamily("Default")]
    public interface IConferenceRepository
    {
        IEnumerable<Conference> GetAllConferences();
        Conference GetConferenceByKey(string key);
        Conference GetFirstConferenceAfterDate(DateTime date);
    }
}