using System.Reflection;
using System.Web.Mvc;
using Castle.Windsor;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Security;
using CodeCampServer.Website;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Helpers;
using CodeCampServer.Website.Impl;
using CodeCampServer.Website.Security;
using NUnit.Framework;

namespace CodeCampServer.UnitTests
{
	public static class TestHelperExtensions
	{
		/// <summary>
		/// Returns true if the method is marked with the [AdminOnly] attribute
		/// </summary>
		/// <param name="method">The method to inspect</param>
		/// <returns>true/false</returns>
		public static bool HasAdminOnlyAttribute(this MethodInfo method)
		{
			//the [AdminOnly] attribute requires windsor to be setup
			IWindsorContainer container = new WindsorContainer();

			IoC.InitializeWith(container);
			IoC.Register<IHttpContextProvider, HttpContextProvider>();
			IoC.Register<IUserSession, FakeUserSession>();

			//TODO:  is there a way to do this that _doesn't_ instantiate the attribute class?  if so we can
			//delete the windsor setup crap above
			object[] attributes = method.GetCustomAttributes(typeof (AdminOnlyAttribute), true);

			IoC.Reset();

			return attributes.Length > 0;
		}

		public static bool RedirectsToAction(this RedirectToRouteResult result, string action)
		{
			return result.Values["action"].ToString().ToLower().Equals(action.ToLower());
		}

		public class FakeUserSession : IUserSession
		{
			public Person GetLoggedInPerson()
			{
				throw new System.NotImplementedException();
			}

			public bool IsAdministrator
			{
				get { throw new System.NotImplementedException(); }
			}

			public void PushUserMessage(FlashMessage.MessageType messageType, string message)
			{
				throw new System.NotImplementedException();
			}

			public FlashMessage[] PopUserMessages()
			{
				throw new System.NotImplementedException();
			}
		}
	}
}