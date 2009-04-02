using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	[TestFixture]
	public class SessionControllerTester : SaveControllerTester
	{
		[Test]
		public void Delete_should_delete_a_Session_and_render_index()
		{
			var conference = new Conference {Key = "foo"};
			var Session = new Session {Conference = conference};
			var repository = S<ISessionRepository>();
			var controller = new SessionController(repository, S<ISessionMapper>(), PermisiveSecurityContext());

			var result = controller.Delete(Session);

			repository.AssertWasCalled(x => x.Delete(Session));
			result.AssertActionRedirect().RedirectsTo<SessionController>(x => x.Index(null)).ShouldBeTrue();
        }

        [Test]
        public void Delete_should_prevent_a_user_who_is_not_an_admin()
        {
            var conference = new Conference { Key = "foo" };
            var Session = new Session { Conference = conference };
            var controller = new SessionController(null, null, RestrictiveSecurityContext());

            var result = controller.Delete(Session);

            result.AssertViewRendered().ForView(ViewPages.NotAuthorized);
        }

		[Test]
		public void Edit_should_but_Session_in_viewdata()
		{
			var Session = new Session();
			var sessionForm = new SessionForm();

			var mapper = S<ISessionMapper>();
			mapper.Stub(m => m.Map(Session)).Return(sessionForm);
			var controller = new SessionController(S<ISessionRepository>(), mapper, PermisiveSecurityContext());

			ViewResult edit = controller.Edit(Session);

			edit.ViewData.Model.ShouldEqual(sessionForm);
			edit.ViewName.ShouldEqual(ViewNames.Default);
		}

        [Test]
        public void Edit_should_only_allow_admins_to_edit_a_session()
        {
            var Session = new Session();

            var controller = new SessionController(null, null, RestrictiveSecurityContext());

            ViewResult edit = controller.Edit(Session);

            edit.ViewName.ShouldEqual(ViewPages.NotAuthorized);
        }

		[Test]
		public void Index_should_put_Sessions_for_conference_in_viewdata()
		{
			var conference = new Conference();
			var repository = S<ISessionRepository>();
			var sessions = new[] {new Session()};
			repository.Stub(x => x.GetAllForConference(conference)).Return(sessions);
			var mapper = S<ISessionMapper>();
			var sessionForms = new[] {new SessionForm()};
			mapper.Stub(m => m.Map(sessions)).Return(sessionForms);
			var controller = new SessionController(repository, mapper, PermisiveSecurityContext());

			ViewResult result = controller.List(conference);

			result.ViewData.Model.ShouldEqual(sessionForms);
			result.ViewName.ShouldEqual(ViewNames.Default);
		}

		[Test]
		public void New_should_put_a_new_Session_form_on_model_and_render_edit_view()
		{
			var controller = new SessionController(S<ISessionRepository>(), S<ISessionMapper>(), PermisiveSecurityContext());
			ViewResult result = controller.New(null);
			result.ViewName.ShouldEqual("Edit");
			result.ViewData.Model.ShouldEqual(new SessionForm());
		}

        [Test]
        public void New_should_prevent_a_user_who_is_not_an_admin()
        {
            var controller = new SessionController(S<ISessionRepository>(), S<ISessionMapper>(), RestrictiveSecurityContext());
            ViewResult result = controller.New(null);
            result.ViewName.ShouldEqual(ViewPages.NotAuthorized);            
        }

		[Test]
		public void Should_not_save_session_if_key_already_exists()
		{
			var form = new SessionForm {Key = "foo", Id = Guid.NewGuid()};
			var session = new Session();

			var mapper = S<ISessionMapper>();
			mapper.Stub(m => m.Map(form)).Return(session);

			var repository = S<ISessionRepository>();
			repository.Stub(r => r.GetByKey("foo")).Return(new Session());

			var controller = new SessionController(repository, mapper, PermisiveSecurityContext());
			var result = (ViewResult) controller.Save(form,null,null);

			result.AssertViewRendered().ViewName.ShouldEqual("Edit");
			controller.ModelState.Values.Count.ShouldEqual(1);
			controller.ModelState["Key"].Errors[0].ErrorMessage.ShouldEqual("This session key already exists");
		}

		[Test]
		public void Should_save_the_session()
		{
			var form = new SessionForm {Conference = new Conference()};
			var session = new Session();

			var mapper = S<ISessionMapper>();
			mapper.Stub(m => m.Map(form)).Return(session);

			var repository = S<ISessionRepository>();

			var controller = new SessionController(repository, mapper, PermisiveSecurityContext());
			var result = (RedirectToRouteResult) controller.Save(form,null,form.Conference);

			repository.AssertWasCalled(r => r.Save(session));
			result.AssertActionRedirect().ToAction<SessionController>(a => a.Index(null));
		}

        [Test]
        public void Should_prevent_user_from_saving_when_the_are_not_an_admin()
        {
            var form = new SessionForm { Conference = new Conference() };
            var session = new Session();

            var mapper = S<ISessionMapper>();
            mapper.Stub(m => m.Map(form)).Return(session);

            var repository = S<ISessionRepository>();

            var controller = new SessionController(repository, mapper,RestrictiveSecurityContext());

            var result = controller.Save(form, null, form.Conference);

            result.AssertViewRendered().ForView(ViewPages.NotAuthorized);
        }
    }
}