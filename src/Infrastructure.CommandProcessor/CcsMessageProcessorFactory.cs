using MvcContrib.CommandProcessor;
using MvcContrib.CommandProcessor.Commands;
using MvcContrib.CommandProcessor.Configuration;
using MvcContrib.CommandProcessor.Interfaces;
using MvcContrib.CommandProcessor.Validation;

namespace CodeCampServer.Infrastructure.CommandProcessor
{
	public class CcsMessageProcessorFactory : IMessageProcessorFactory {
		public IMessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine, CommandEngineConfiguration configuration)
		{
			return new MessageProcessor(mappingEngine,CreateInvoker(new CcsCommandFactory()), unitOfWork, configuration);
		}

		private CommandInvoker CreateInvoker(ICommandFactory commandFactory)
		{
			return new CommandInvoker(new ValidationEngine(new ValidationRuleFactory()),commandFactory);
		}
	}
}