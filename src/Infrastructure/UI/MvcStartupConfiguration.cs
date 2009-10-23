using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.UI.Binders;
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
			InputBuilder.SetConventionProvider(() => new InputBuilderConventions());
			ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
			ModelBinders.Binders.DefaultBinder = new SmartBinder();
			ModelBinders.Binders.Add(typeof (UserGroup),
			                         DependencyRegistrar.Resolve<UserGroupModelBinder>());
		}
	}
}