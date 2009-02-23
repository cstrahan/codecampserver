using CodeCampServer.Core.Domain.Model.Planning;

namespace CodeCampServer.Core.Services
{
	public interface IStateCommandFactory
	{
		IStateCommand[] GetAllStateCommands();
	}
}