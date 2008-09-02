using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.UnitTests
{
	public static class TestHelperExtensions
	{
		public static bool HasAdminAuthorizationAttribute(this MethodInfo method)
		{
		    return method.GetAttribute<AuthorizeAttribute>().Roles.Contains("Administrator");			
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