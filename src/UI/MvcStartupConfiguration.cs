using System;
using System.Web.Mvc;
using CodeCampServer.Core;
using CodeCampServer.UI.Binders;
using CodeCampServer.UI.InputBuilders;
using MvcContrib;
using MvcContrib.UI.InputBuilder;

namespace CodeCampServer.UI
{
	public class MvcStartupConfiguration : AbstractFactoryBase<CompositionBinder>, IRequiresConfigurationOnStartup
	{
		public static Func<CompositionBinder> GetDefault = DefaultUnconfiguredState;

		public void Configure()
		{
			InputBuilder.BootStrap();
			InputBuilder.SetPropertyConvention(() => new InputBuilderPropertyFactory());
			ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
			ModelBinders.Binders.DefaultBinder = GetDefault();

			Bus.AddMessageHandler(typeof (LoginHandler));
			Bus.Instance.SetMessageHandlerFactory(new ConventionMessageHandlerFactory());

			new RouteConfigurator().RegisterRoutes(AreaRegistration.RegisterAllAreas);
		}
	}
}