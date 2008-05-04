using System.Reflection;
using Castle.Windsor;
using CodeCampServer.Model.Security;
using CodeCampServer.Website;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Helpers;
using CodeCampServer.Website.Security;

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
            IoC.Register<IAuthorizationService, AuthorizationService>();

            //TODO:  is there a way to do this that _doesn't_ instantiate the attribute class?  if so we can
            //delete the windsor setup crap above
            var attributes = method.GetCustomAttributes(typeof (AdminOnlyAttribute), true);            

            IoC.Reset();

            return attributes.Length > 0;
        }        
    }    
}
