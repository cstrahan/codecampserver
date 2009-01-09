using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using MvcContrib;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class AdminControllerTester : TestControllerBase
	{
		[Test]
		public void
			When_an_admin_object_does_not_exist_Contoller_should_redirect_to_admin_password_form_when_there_are_zero_users
			()
		{
			var repository = S<IUserRepository>();
			repository.Stub(repo => repo.GetByUserName("admin")).Return(null);

			var controller = new AdminController(repository, S<IUserUpdater>());
			var result = controller.Index();
			result.AssertActionRedirect();

			var redirectResult = result as RedirectToRouteResult;
			redirectResult.ToAction<AdminController>(a => a.Edit(null));
		}

		[Test]
		public void When_a_user_does_not_exist_Edit__should_render_the_edit_view()
		{
			var repository = S<IUserRepository>();
			repository.Stub(repo => repo.GetByUserName("admin")).Return(null);
			repository.Stub(repo => repo.GetAll()).Return(new User[0]);

			var controller = new AdminController(repository, S<IUserUpdater>());
			var result = controller.Edit(null);
			result.AssertViewRendered().ForView(DEFAULT_VIEW);
			result.ViewData.Get<User>().ShouldNotBeNull();
		}

		[Test]
		public void When_an_admin_object_exists_Save_should_a_valid_user()
		{
			var user = new User {Username = "admin", Id = Guid.NewGuid()};
			var updater = S<IUserUpdater>();
			var form = new UserForm{Id = user.Id, Password = "pass"};
			updater.Stub(u => u.UpdateFromMessage(form)).Return(new UpdateResult<User, IUserMessage>(true, user));
			var controller = new AdminController(S<IUserRepository>(), updater);

			var result = (RedirectToRouteResult) controller.Save(form);

			result.AssertActionRedirect()
				.ToAction<AdminController>(a => a.Index());
		}
	}
}