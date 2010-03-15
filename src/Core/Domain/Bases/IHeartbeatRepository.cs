namespace CodeCampServer.Core.Domain.Bases
{
	public interface IHeartbeatRepository : IRepository<Heartbeat>
	{
		Heartbeat GetLatest();
		Heartbeat[] GetTop();
	}
}