using System;
using System.Collections.Generic;
using CodeCampServer.Domain.Model;
using StructureMap;

namespace CodeCampServer.Domain
{
    [PluginFamily("Default")]
    public interface IConferenceRepository
    {
        IEnumerable<Conference> GetAllEvents();
        Conference GetEventByKey(string key);
        Conference GetFirstEventAfterDate(DateTime date);
    }
}