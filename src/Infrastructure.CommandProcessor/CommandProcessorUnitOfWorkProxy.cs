using Tarantino.RulesEngine;

namespace CodeCampServer.Infrastructure.CommandProcessor
{
    public class CommandProcessorUnitOfWorkProxy : IUnitOfWork
    {
        private readonly Core.IUnitOfWork _coreUnitOfWork;

        public CommandProcessorUnitOfWorkProxy(Core.IUnitOfWork coreUnitOfWork)
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