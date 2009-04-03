using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	[TestFixture]
	public abstract class SaveControllerTester : TestBase
	{
        protected ISecurityContext PermisiveSecurityContext()
        {
            var context = S<ISecurityContext>();
            SetSecurityReturn(context, true);
            return context;
        }

        protected ISecurityContext RestrictiveSecurityContext()
        {
            var context = S<ISecurityContext>();
            SetSecurityReturn(context, false);
            return context;
        }

	    private void SetSecurityReturn(ISecurityContext context, bool allowPermision) {
            context.Stub(securityContext => securityContext.HasPermissionsFor(new Session())).IgnoreArguments().
                Return(allowPermision).Repeat.Any();
            context.Stub(securityContext => securityContext.HasPermissionsFor(new Conference())).IgnoreArguments().
	            Return(allowPermision).Repeat.Any();
	        context.Stub(securityContext => securityContext.HasPermissionsFor(new Speaker())).IgnoreArguments().
	            Return(allowPermision).Repeat.Any();
	        context.Stub(securityContext => securityContext.HasPermissionsFor(new UserGroup())).IgnoreArguments().
	            Return(allowPermision).Repeat.Any();
	        context.Stub(securityContext => securityContext.HasPermissionsForUserGroup(Guid.Empty)).IgnoreArguments().
	            Return(allowPermision).Repeat.Any();
	        context.Stub(securityContext => securityContext.IsAdmin()).Return(allowPermision).Repeat.Any();
	    }
	}

    public static class ActionResultSecutiryExtension
    {
        public static void ShouldBeNotAuthorized(this ActionResult result )
        {
            result.AssertViewRendered().ForView(ViewPages.NotAuthorized);
        }

    }
}