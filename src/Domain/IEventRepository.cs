using System;
using System.Collections.Generic;
using CodeCampServer.Domain.Model;
using StructureMap;

namespace CodeCampServer.Domain
{
    [PluginFamily("Default")]
    public interface IEventRepository
    {
        IEnumerable<Event> GetAllEvents();
        Event GetEventByKey(string key);
        Event GetFirstEventAfterDate(DateTime date);
    }
}