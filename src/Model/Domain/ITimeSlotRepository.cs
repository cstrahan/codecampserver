namespace CodeCampServer.Model.Domain
{
    public interface ITimeSlotRepository
	{
	    void Save(TimeSlot session);

        TimeSlot[] GetTimeSlotsFor(Conference conference);
	}
}
