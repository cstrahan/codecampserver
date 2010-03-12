namespace CodeCampServer.Core.Services
{
	public interface IHeartbeatChecker
	{
		string CheckHeartbeat(int timeout);
	}
}