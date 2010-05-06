using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core;
using CodeCampServer.Core.Services.BusinessRule;
using CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration;
using MvcContrib.CommandProcessor;
using MvcContrib.CommandProcessor.Commands;
using MvcContrib.CommandProcessor.Configuration;
using MvcContrib.CommandProcessor.Interfaces;
using MvcContrib.CommandProcessor.Validation;
using IUnitOfWork=MvcContrib.CommandProcessor.Interfaces.IUnitOfWork;

namespace CodeCampServer.Infrastructure.CommandProcessor
{
	public class RulesEngineConfiguration : IRequiresConfigurationOnStartup
	{
		public static void Configure(Type typeToLocatorConfigurationAssembly)
		{
			var rulesEngine = new MvcContrib.CommandProcessor.RulesEngine();
			rulesEngine.Initialize(typeToLocatorConfigurationAssembly.Assembly, new CcsMessageMapper());
			MvcContrib.CommandProcessor.RulesEngine.MessageProcessorFactory = new CcsMessageProcessorFactory();
				//new MessageProcessorFactory();
		}

		public class CcsMessageMapper : IMessageMapper
		{
			public object MapUiMessageToCommandMessage(object message, Type messageType, Type destinationType)
			{
				return Mapper.Map(message, message.GetType(), destinationType);
			}
		}

		public void Configure()
		{
			Configure(typeof (DeleteMeetingMessageConfiguration));
		}
	}

	public class CcsMessageProcessorFactory : IMessageProcessorFactory {
		public IMessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine, CommandEngineConfiguration configuration)
		{
			return new MessageProcessor(mappingEngine,
							new CommandInvoker(new ValidationEngine(new ValidationRuleFactory()),
											   new CcsCommandFactory()), unitOfWork, configuration);

		}
	}

	public class CcsCommandFactory : ICommandFactory {

		public static Func<Type, ICommand[]> CommandLocator = (t) => { throw new NotImplementedException(); };

		public IEnumerable<ICommandMessageHandler> GetCommands(ICommandConfiguration definition)
		{
			Type concreteCommandType = typeof(ICommand<>).MakeGenericType(definition.CommandMessageType);
			var commands = CommandLocator(concreteCommandType);
			return commands.Select(command => new CommandMessageHandlerProxy(command)).ToArray();
		}
	}

	public class CommandMessageHandlerProxy:ICommandMessageHandler
	{
		private readonly ICommand _command;

		public CommandMessageHandlerProxy(ICommand command)
		{
			_command = command;
		}

		public ReturnValue Execute(object commandMessage)
		{
			var value = _command.Execute(commandMessage);
			return new ReturnValue{Type = value.GetType(),Value = value};
		}
	}
}