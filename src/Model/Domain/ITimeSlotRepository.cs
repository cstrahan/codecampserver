using System;
using System.Collections.Generic;
using StructureMap;

namespace CodeCampServer.Model.Domain
{
    [PluginFamily(Keys.DEFAULT)]
    public interface ITimeSlotRepository
	{
	    void Save(TimeSlot session);

        TimeSlot[] GetTimeSlotsFor(Conference conference);
	}
}
