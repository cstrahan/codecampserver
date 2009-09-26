using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Helpers.Binders;
using CodeCampServer.UI.Views;

namespace CodeCampServer.UI
{
	public class GlobalApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			new RouteConfigurator().RegisterRoutes();
		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
			AutoMapperConfiguration.Configure();
			MvcContrib.UI.InputBuilder.InputBuilder.BootStrap();
			MvcContrib.UI.InputBuilder.InputBuilder.SetConventionProvider(() => new InputBuilderConventions());
			ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());

			ModelBinders.Binders.DefaultBinder = new SmartBinder();
			DependencyRegistrar.EnsureDependenciesRegistered();
			ModelBinders.Binders.Add(typeof (UserGroup),
			                         DependencyRegistrar.Resolve<UserGroupModelBinder>());
		}
	}
}