using CodeCampServer.Core;

namespace CodeCampServer.Infrastructure.CommandProcessor
{
	public class CommandProcessorUnitOfWorkProxy : MvcContrib.CommandProcessor.Interfaces.IUnitOfWork
	{
		private readonly IUnitOfWork _coreUnitOfWork;

		public CommandProcessorUnitOfWorkProxy(IUnitOfWork coreUnitOfWork)
		{
			_coreUnitOfWork = coreUnitOfWork;
		}

		public void Dispose()
		{
			_coreUnitOfWork.Dispose();
		}

		public void Invalidate()
		{
			_coreUnitOfWork.RollBack();
		}
	}
}