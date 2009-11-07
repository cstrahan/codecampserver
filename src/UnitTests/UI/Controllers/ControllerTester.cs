using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	[TestFixture]
	public abstract class ControllerTester : TestBase
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

		private static void SetSecurityReturn(ISecurityContext context, bool allowPermision)
		{
			context.Stub(securityContext => securityContext.HasPermissionsFor(new UserGroup())).IgnoreArguments().
				Return(allowPermision).Repeat.Any();
			context.Stub(securityContext => securityContext.HasPermissionsForUserGroup(Guid.Empty)).IgnoreArguments().
				Return(allowPermision).Repeat.Any();
			context.Stub(securityContext => securityContext.IsAdmin()).Return(allowPermision).Repeat.Any();
		}
	}
}