using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Services.BusinessRule;
using MvcContrib.CommandProcessor.Commands;
using MvcContrib.CommandProcessor.Interfaces;

namespace CodeCampServer.Infrastructure.CommandProcessor
{
	public class CcsCommandFactory : ICommandFactory {

		public static Func<Type, ICommandHandler[]> CommandLocator = (t) => { throw new NotImplementedException(); };

		public IEnumerable<ICommandMessageHandler> GetCommands(ICommandConfiguration definition)
		{
			Type concreteCommandType = typeof(ICommandHandler<>).MakeGenericType(definition.CommandMessageType);
			var commands = CommandLocator(concreteCommandType);
			return commands.Select(command => new CommandMessageHandlerProxy(command)).ToArray();
		}
	}
}