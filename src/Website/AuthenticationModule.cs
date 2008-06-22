using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace CodeCampServer.Website
{
	public class AuthenticationModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += context_AuthenticateRequest;
		}

		private void context_AuthenticateRequest(object sender, EventArgs e)
		{
			HttpContext context = ((HttpApplication) sender).Context;
			if (context.User == null || !context.User.Identity.IsAuthenticated) return;

			if (context.User.Identity is FormsIdentity)
			{
				var formsIdentity = (FormsIdentity) context.User.Identity;
				FormsAuthenticationTicket ticket = formsIdentity.Ticket;
				string[] roles = ticket.UserData.Split(',');

				context.User = new GenericPrincipal(formsIdentity, roles);
			}
		}

		public void Dispose()
		{
		}
	}
}