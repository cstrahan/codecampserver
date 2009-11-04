using System.Web.Mvc;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.UI.Binders;
using CodeCampServer.Infrastructure.UI.InputBuilders;
using CodeCampServer.Infrastructure.UI.Services;
using CodeCampServer.UI;
using MvcContrib.UI.InputBuilder;

namespace CodeCampServer.Infrastructure.UI
{
	public class MvcStartupConfiguration : IRequiresConfigurationOnStartup
	{
		public void Configure()
		{
			new RouteConfigurator().RegisterRoutes();

			InputBuilder.BootStrap();
			InputBuilder.SetPropertyConvention(() => new InputBuilderPropertyFactory());
			ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
			ModelBinders.Binders.DefaultBinder = new SmartBinder();

			MvcContrib.Services.DependencyResolver.InitializeWith( new StructureMapServiceLocator());
		}
	}
}