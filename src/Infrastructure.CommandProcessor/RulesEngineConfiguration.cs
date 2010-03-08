using System;
using AutoMapper;
using CodeCampServer.Core;
using CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration;
using MvcContrib.CommandProcessor;

namespace CodeCampServer.Infrastructure.CommandProcessor
{
	public class RulesEngineConfiguration : IRequiresConfigurationOnStartup
	{
		public static void Configure(Type typeToLocatorConfigurationAssembly)
		{
			var rulesEngine = new MvcContrib.CommandProcessor.RulesEngine();
			rulesEngine.Initialize(typeToLocatorConfigurationAssembly.Assembly, new CcsMessageMapper());
			MvcContrib.CommandProcessor.RulesEngine.MessageProcessorFactory = new MessageProcessorFactory();
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
}