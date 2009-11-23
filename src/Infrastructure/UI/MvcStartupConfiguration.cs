using System;
using System.Web.Mvc;
using CodeCampServer.Core;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.UI.Binders;
using CodeCampServer.Infrastructure.UI.InputBuilders;
using CodeCampServer.Infrastructure.UI.Services;
using CodeCampServer.UI;
using CodeCampServer.UI.Models.Input;
using LoginPortableArea.Messages;
using LoginPortableArea.Models;
using MvcContrib;
using MvcContrib.PortableAreas;
using MvcContrib.Services;
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

			DependencyResolver.InitializeWith(new StructureMapServiceLocator());

			Bus.AddMessageHandler(typeof (LoginHandler));
			Bus.Instance.SetMessageHandlerFactory(new ConventionMessageHandlerFactory());

			new RouteConfigurator().RegisterRoutes(AreaRegistration.RegisterAllAreas);
		}
	}

	public class ConventionMessageHandlerFactory : IMessageHandlerFactory
	{
		public static Func<Type, object> CreateDependencyCallback = (t) => (IMessageHandler) Activator.CreateInstance(t);

		public IMessageHandler Create(Type type)
		{
			return (IMessageHandler) CreateDependencyCallback(type);
		}
	}

	public class LoginHandler : MessageHandler<LoginInputMessage>
	{
		private IRulesEngine _rulesEngine;
		public LoginHandler(IRulesEngine rulesEngine)
		{
			_rulesEngine = rulesEngine;
		}

		public override void Handle(LoginInputMessage message)
		{
			
			var uimessage = new LoginInput() {Password = message.Input.Password, Username = message.Input.Username};
			
			ICanSucceed result = _rulesEngine.Process(uimessage);
			
			if (result.Successful)
			{
				message.Result.Success = true;
				message.Result.Username = message.Input.Username;				
			}
			foreach (ErrorMessage errorMessage in result.Errors)
			{
				message.Result.Message += errorMessage.Message;
			}
		}
	}
}