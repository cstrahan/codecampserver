using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core;
using CodeCampServer.Core.Services.BusinessRule;
using CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration;
using CodeCampServer.UI.Models.Input;
using MvcContrib.CommandProcessor;
using MvcContrib.CommandProcessor.Configuration;

namespace CodeCampServer.Infrastructure.CommandProcessor
{
	public class RulesEngineConfiguration : IRequiresConfigurationOnStartup
	{
		public void Configure()
		{
			Configure(typeof (UpdateUserConfiguration));
		}

		public static void Configure(Type typeToLocatorConfigurationAssembly)
		{
			var rulesEngine = new MvcContrib.CommandProcessor.RulesEngine();
			rulesEngine.Initialize(typeToLocatorConfigurationAssembly.Assembly, new CcsMessageMapper());
			ConventionConfiguration(rulesEngine.Configuration, typeof (UserInput), typeof (UpdateUserCommandMessage));
			MvcContrib.CommandProcessor.RulesEngine.MessageProcessorFactory = new CcsMessageProcessorFactory();
		}

		private static void ConventionConfiguration(CommandEngineConfiguration configuration, Type inputType, Type messageType)
		{
			var inputTypes = inputType.Assembly.GetTypes()
							.Where(t=>TypeIsAnInputModel(t, inputType)).ToArray();

			var messageTypes = messageType.Assembly.GetTypes()
					.Where(t =>TypeIsACommandMessage(t, messageType));
			                                 
			foreach (Type input in inputTypes)
			{
				if (!configuration.MessageConfigurations.ContainsKey(input))
				{
					Type actualMessageType = messageTypes.FirstOrDefault(type => InputToCommandMessage(type, input));
					if (actualMessageType!=null)
					{
						configuration.MessageConfigurations.Add(input, new ConventionMessageConfiguration(actualMessageType));
					}
				}
			}
		}

		private static bool TypeIsACommandMessage(Type t, Type messageType)
		{
			return !t.IsAbstract && t.Namespace.Equals(messageType.Namespace) && t.Name.EndsWith("CommandMessage");
		}

		private static bool TypeIsAnInputModel(Type t, Type inputType)
		{
			return t!=null&&!t.IsAbstract && t.Namespace!=null && t.Namespace.Equals(inputType.Namespace) && t.Name!=null && t.Name.EndsWith("Input");
		}
		private static bool InputToCommandMessage(Type message, Type input)
		{
			return message.Name.Replace("CommandMessage", "").Equals(input.Name.Replace("Input", ""));
		}

		public class CcsMessageMapper : IMessageMapper
		{
			public object MapUiMessageToCommandMessage(object message, Type messageType, Type destinationType)
			{
				return Mapper.Map(message, message.GetType(), destinationType);
			}
		}
	}
}