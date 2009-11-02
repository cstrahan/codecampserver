using System;
using AutoMapper;
using CodeCampServer.DependencyResolution;
using CommandProcessor;
using Microsoft.Practices.ServiceLocation;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Infrastructure.BusinessRules
{
	public class RulesEngineConfiguration : IRequiresConfigurationOnStartup
	{
		public static void Configure(Type typeToLocatorConfigurationAssembly)
		{
			ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
			var rulesEngine = new RulesEngine();
			RulesEngine.MessageProcessorFactory = new MessageProcessorFactory();
			rulesEngine.Initialize(typeToLocatorConfigurationAssembly.Assembly, new CcsMessageMapper());
		}

		private class CcsMessageMapper : IMessageMapper
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