using System.Web.Mvc;
using CodeCampServer.Core;
using CodeCampServer.Infrastructure.UI.Binders;
using CodeCampServer.Infrastructure.UI.InputBuilders;
using CodeCampServer.Infrastructure.UI.Services;
using CodeCampServer.UI;
using MvcContrib;
using MvcContrib.UI.InputBuilder;

namespace CodeCampServer.Infrastructure.UI
{
	public class MvcStartupConfiguration : IRequiresConfigurationOnStartup
	{
		public void Configure()
		{
			InputBuilder.BootStrap();
			InputBuilder.SetPropertyConvention(() => new InputBuilderPropertyFactory());
			ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
			ModelBinders.Binders.DefaultBinder = new SmartBinder();


			Bus.AddMessageHandler(typeof (LoginHandler));
			Bus.Instance.SetMessageHandlerFactory(new ConventionMessageHandlerFactory());

			new RouteConfigurator().RegisterRoutes(AreaRegistration.RegisterAllAreas);
		}
	}
}