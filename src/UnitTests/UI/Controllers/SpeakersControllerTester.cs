using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.Core.Services.Updaters.Impl;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;
namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class SpeakerControllerTester : TestControllerBase
	{
		[Test]
		public void Index_should_put_speakers_in_viewdata()
		{
			var repository = M<ISpeakerRepository>();
			var speakers = new[] {new Speaker()};
			repository.Stub(x => x.GetAll()).Return(speakers);
			var controller = new SpeakerController(repository, M<ISpeakerUpdater>());

			var result = controller.Index();

			result.AssertViewRendered()
				.ForView(ViewNames.Default)
                .ViewData.Get<Speaker[]>()
				.ShouldEqual(speakers);			
		}

		[Test]
		public void Edit_should_but_speaker_in_viewdata()
		{
			var speaker = new Speaker();
			var controller = new SpeakerController(M<ISpeakerRepository>(), M<ISpeakerUpdater>());

			ActionResult edit = controller.Edit(speaker);

			edit.AssertViewRendered()
				.ForView(ViewNames.Default)
				.ViewData.Get<Speaker>().ShouldEqual(speaker);
			
		}

		[Test]
		public void Save_test_a_vaild_save()
		{
			var form = new SpeakerForm();
			var updater = M<ISpeakerUpdater>();
			updater.Stub(x => x.UpdateFromMessage(form)).Return(ModelUpdater<Speaker, ISpeakerMessage>.Success());
			var controller = new SpeakerController(M<ISpeakerRepository>(), updater);

			ActionResult result = controller.Save(form);

			result.AssertActionRedirect()
				.ToAction<SpeakerController>(x => x.Index());

			updater.AssertWasCalled(c=>c.UpdateFromMessage(form));
		}

		[Test]
		public void Save_test_a_invaild_save()
		{
			var form = new SpeakerForm();
			var updater = M<ISpeakerUpdater>();
			updater.Stub(x => x.UpdateFromMessage(form)).Return(ModelUpdater<Speaker, ISpeakerMessage>.Fail().WithMessage(
																	x => x.FirstName, "Some Message"));
			var controller = new SpeakerController(M<ISpeakerRepository>(), updater);


			ActionResult result = controller.Save(form);
			
			result.AssertViewRendered()
				.ForView("Edit")
				.ViewData.ModelState.ContainsKey("FirstName").ShouldBeTrue();
			
		}

		[Test]
		public void New_should_put_a_new_speaker_form_on_model_and_render_edit_view()
		{
			var controller = new SpeakerController(S<ISpeakerRepository>(), S<ISpeakerUpdater>());
			ActionResult result = controller.New();
			
			result.AssertViewRendered()
				.ForView("Edit")			
				.ViewData.Model.ShouldBeAssignableFrom(typeof(SpeakerForm));
				
		}

		[Test]
		public void Delete_should_delete_a_speaker_and_render_index()
		{
			var speaker = new Speaker {};
			var repository = S<ISpeakerRepository>();
			var controller = new SpeakerController(repository, S<ISpeakerUpdater>());

			ActionResult result = controller.Delete(speaker);

			result.AssertActionRedirect()
				.ToAction<SpeakerController>(x => x.Index());
				

			repository.AssertWasCalled(x => x.Delete(speaker));
		}
	}
}