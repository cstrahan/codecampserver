using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class SpeakerControllerTester : SaveControllerTester
	{
		[Test]
		public void Index_should_put_speakers_in_viewdata()
		{
			var repository = M<ISpeakerRepository>();
			var speakers = new[] {new Speaker()};
			var speakerForms = new[] {new SpeakerForm()};
			repository.Stub(x => x.GetAll()).Return(speakers);
			var mapper = S<ISpeakerMapper>();
			mapper.Stub(m => m.Map(speakers)).Return(speakerForms);
			var controller = new SpeakerController(repository, mapper);

			var result = controller.Index();

			result.AssertViewRendered().ForView(ViewNames.Default);
			var forms = ((SpeakerForm[])controller.ViewData.Model);
			forms.ShouldEqual(speakerForms);
		}

		[Test]
		public void Edit_should_but_speaker_in_viewdata()
		{
			var speaker = new Speaker();
			var mapper = S<ISpeakerMapper>();
			var speakerForm = new SpeakerForm();
			mapper.Stub(m => m.Map(speaker)).Return(speakerForm);
			var controller = new SpeakerController(S<ISpeakerRepository>(), mapper);

			ActionResult edit = controller.Edit(speaker);

			edit.AssertViewRendered().ForView(ViewNames.Default);
			var form = ((SpeakerForm)controller.ViewData.Model);
			form.ShouldEqual(speakerForm);
		}

		[Test]
		public void Should_save_the_speaker()
		{
			var form = new SpeakerForm();
			var speaker = new Speaker();

			var mapper = S<ISpeakerMapper>();
			mapper.Stub(m => m.Map(form)).Return(speaker);

			var repository = S<ISpeakerRepository>();

			var controller = new SpeakerController(repository, mapper);
			var result = (RedirectToRouteResult) controller.Save(form);

			repository.AssertWasCalled(r => r.Save(speaker));
			result.AssertActionRedirect().ToAction<SpeakerController>(a => a.Index());
		}

		[Test]
		public void Should_not_save_speaker_if_key_already_exists()
		{
			var form = new SpeakerForm {Key = "foo", Id = Guid.NewGuid()};
			var speaker = new Speaker();

			var mapper = S<ISpeakerMapper>();
			mapper.Stub(m => m.Map(form)).Return(speaker);

			var repository = S<ISpeakerRepository>();
			repository.Stub(r => r.GetByKey("foo")).Return(new Speaker());

			var controller = new SpeakerController(repository, mapper);
			var result = (ViewResult) controller.Save(form);

			result.AssertViewRendered().ViewName.ShouldEqual("Edit");
			controller.ModelState.Values.Count.ShouldEqual(1);
			controller.ModelState["Key"].Errors[0].ErrorMessage.ShouldEqual("This speaker key already exists");
		}

		[Test]
		public void New_should_put_a_new_speaker_form_on_model_and_render_edit_view()
		{
			var controller = new SpeakerController(S<ISpeakerRepository>(), S<ISpeakerMapper>());
			ActionResult result = controller.New();

			result.AssertViewRendered()
				.ForView("Edit")
				.ViewData.Model.ShouldBeAssignableFrom(typeof (SpeakerForm));
		}

		[Test]
		public void Delete_should_delete_a_speaker_and_render_index()
		{
			var speaker = new Speaker();
			var repository = S<ISpeakerRepository>();
			var controller = new SpeakerController(repository, S<ISpeakerMapper>());

			ActionResult result = controller.Delete(speaker);

			result.AssertActionRedirect()
				.ToAction<SpeakerController>(x => x.Index());


			repository.AssertWasCalled(x => x.Delete(speaker));
		}
	}
}