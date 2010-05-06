using CodeCampServer.Core;
using CodeCampServer.Core.Services.BusinessRule;
using CodeCampServer.Infrastructure.Automapper.ObjectMapping;
using CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles;
using CodeCampServer.Infrastructure.CommandProcessor;
using CodeCampServer.Infrastructure.NHibernate;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using CodeCampServer.UI;
using CodeCampServer.UI.Binders;
using CodeCampServer.UI.Binders.Entities;
using CodeCampServer.UI.Binders.Keyed;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.InputBuilders;
using CodeCampServer.UI.InputBuilders.SelectListProvision;
using CodeCampServer.UI.Services;
using MvcContrib.Services;
using StructureMap;
using Microsoft.Practices.ServiceLocation;
using System.Linq;

namespace CodeCampServer.DependencyResolution
{
	public class InitiailizeDefaultFactories : IRequiresConfigurationOnStartup
	{
		public void Configure()
		{
			//what a mess this is huh?  how about a reflection based way to find all static methods with this 
			//name and signature and just replace them with this.
			ContainerBaseActionFilter.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);

			MvcStartupConfiguration.GetDefault = () => ObjectFactory.GetInstance<CompositionBinder>();
			EntityModelBinderFactory.GetDefault = type => (IEntityModelBinder) ObjectFactory.GetInstance(type);
			KeyedModelBinderFactory.GetDefault = type => (IKeyedModelBinder) ObjectFactory.GetInstance(type);

			SessionBuilder.GetDefault = () => ObjectFactory.GetInstance<ChangeAuditInfoInterceptor>();
			InputBuilderPropertyFactory.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			MeetingMapperProfile.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			AutoMapperConfiguration.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			ConventionMessageHandlerFactory.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			ControllerFactory.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);
			DependencyResolver.InitializeWith(new StructureMapServiceLocator());
			ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
			UnitOfWorkFactory.GetDefault = () => ObjectFactory.GetInstance<IUnitOfWork>();
			SelectListProviderFactory.GetDefault = type => (ISelectListProvider) ObjectFactory.GetInstance(type);
			ReturnUrlManagerFactory.GetDefault = () => ObjectFactory.GetInstance<IReturnUrlManager>();
			CcsCommandFactory.CommandLocator = (t) => ObjectFactory.GetAllInstances(t).Cast<ICommandHandler>().ToArray();
		}
	}
}