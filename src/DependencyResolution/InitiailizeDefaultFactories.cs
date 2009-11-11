using CodeCampServer.Core;
using CodeCampServer.Infrastructure.DataAccess;
using CodeCampServer.Infrastructure.ObjectMapping;
using CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles;
using CodeCampServer.Infrastructure.UI;
using CodeCampServer.Infrastructure.UI.Binders;
using CodeCampServer.Infrastructure.UI.InputBuilders;
using CodeCampServer.Infrastructure.UI.Services;
using CodeCampServer.UI.Filters;
using StructureMap;

namespace CodeCampServer.DependencyResolution
{
	public class InitiailizeDefaultFactories : IRequiresConfigurationOnStartup
	{
		public void Configure()
		{
			//what a mess this is huh?  how about a reflection based way to find all static methods with this 
			//name and signature and just replace them with this.
			ContainerBaseActionFilter.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			SmartBinder.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			ConfigBasedSessionSource.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			InputBuilderPropertyFactory.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			MeetingMapperProfile.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			AutoMapperConfiguration.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			ConventionMessageHandlerFactory.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			ControllerFactory.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
		}
	}
}