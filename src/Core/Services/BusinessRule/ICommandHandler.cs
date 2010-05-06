namespace CodeCampServer.Core.Services.BusinessRule
{
	public interface ICommandHandler<TCommand>:ICommandHandler
	{
		object Execute(TCommand commandMessage);
	}
	public interface ICommandHandler
	{
		
	}

}