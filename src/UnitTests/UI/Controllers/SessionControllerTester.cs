using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	[TestFixture]
	public class SessionControllerTester : TestControllerBase
	{
		[Test]
		public void Index_should_put_Sessions_for_conference_in_viewdata()
		{
			var conference = new Conference();
			var repository = S<ISessionRepository>();
			var Sessions = new[] {new Session()};
			repository.Stub(x => x.GetAllForConference(conference)).Return(Sessions);
			var controller = new SessionController(repository, S<ISessionUpdater>());

			ViewResult result = controller.Index(conference);

			result.ViewData.Get<Session[]>().ShouldEqual(Sessions);
			result.ViewName.ShouldEqual(ViewNames.Default);
		}

		[Test]
		public void Edit_should_but_Session_in_viewdata()
		{
			var Session = new Session();
			var controller = new SessionController(S<ISessionRepository>(), S<ISessionUpdater>());

			ViewResult edit = controller.Edit(Session);

			edit.ViewData.Get<Session>().ShouldEqual(Session);
			edit.ViewName.ShouldEqual(ViewNames.Default);
		}

		[Test]
		public void Save_test_a_vaild_save()
		{
			var form = new SessionForm(){Conference = new Conference()};
			var updater = S<ISessionUpdater>();
			updater.Stub(x => x.UpdateFromMessage(form)).Return(ModelUpdater<Session, ISessionMessage>.Success());
			var controller = new SessionController(S<ISessionRepository>(), updater);

			var result = (RedirectToRouteResult) controller.Save(form);

			result.RedirectsTo<SessionController>(x => x.Index(null)).ShouldBeTrue();
		}

		[Test]
		public void Save_test_a_invaild_save()
		{
			var form = new SessionForm();
			var updater = S<ISessionUpdater>();
			updater.Stub(x => x.UpdateFromMessage(form)).Return(ModelUpdater<Session, ISessionMessage>.Fail().WithMessage(
			                                                    	x => x.Title, "Some Message"));
			var controller = new SessionController(S<ISessionRepository>(), updater);


			var result = (ViewResult) controller.Save(form);
			result.ViewData.ModelState.ContainsKey("Title").ShouldBeTrue();
			result.ViewName.ShouldEqual("Edit");
		}

		[Test]
		public void New_should_but_a_new_Session_form_on_model_and_render_edit_view()
		{
			var controller = new SessionController(S<ISessionRepository>(), S<ISessionUpdater>());
			ViewResult result = controller.New();
			result.ViewName.ShouldEqual("Edit");
			result.ViewData.Model.ShouldEqual(new SessionForm());
		}

		[Test]
		public void Delete_should_delete_a_Session_and_render_index()
		{
			var conference = new Conference {Key = "foo"};
			var Session = new Session {Conference = conference};
			var repository = S<ISessionRepository>();
			var controller = new SessionController(repository, S<ISessionUpdater>());

			RedirectToRouteResult result = controller.Delete(Session);

			repository.AssertWasCalled(x => x.Delete(Session));
			result.RedirectsTo<SessionController>(x => x.Index(null)).ShouldBeTrue();
		}
	}
}