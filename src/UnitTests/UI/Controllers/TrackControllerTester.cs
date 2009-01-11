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
	public class TrackControllerTester : SaveControllerTester
	{
		[Test]
		public void Index_should_put_tracks_for_conference_in_viewdata()
		{
			var conference = new Conference();
			var repository = S<ITrackRepository>();
			var tracks = new[] {new Track()};
			repository.Stub(x => x.GetAllForConference(conference)).Return(tracks);
			var mapper = S<ITrackMapper>();
			var trackForms = new[] {new TrackForm()};
			mapper.Stub(m => m.Map(tracks)).Return(trackForms);
			var controller = new TrackController(repository, mapper);

			ViewResult result = controller.Index(conference);

			result.ViewName.ShouldEqual(ViewNames.Default);
			var forms = ((TrackForm[])controller.ViewData.Model);
			forms.ShouldEqual(trackForms);
		}

		[Test]
		public void Edit_should_but_track_in_viewdata()
		{
			var track = new Track();
			var mapper = S<ITrackMapper>();
			var trackForm = new TrackForm();
			mapper.Stub(m => m.Map(track)).Return(trackForm);
			var controller = new TrackController(S<ITrackRepository>(), mapper);

			ViewResult edit = controller.Edit(track);

			edit.ViewName.ShouldEqual(ViewNames.Default);
			var form = ((TrackForm)controller.ViewData.Model);
			form.ShouldEqual(trackForm);
		}

		[Test]
		public void Should_save_the_track()
		{
			var form = new TrackForm();
			var track = new Track();

			var mapper = S<ITrackMapper>();
			mapper.Stub(m => m.Map(form)).Return(track);

			var repository = S<ITrackRepository>();

			var controller = new TrackController(repository, mapper);
			var result = (RedirectToRouteResult)controller.Save(form);

			repository.AssertWasCalled(r => r.Save(track));
			result.AssertActionRedirect().ToAction<TrackController>(a => a.Index(null));
		}

		[Test]
		public void New_should_but_a_new_track_form_on_model_and_render_edit_view()
		{
			var controller = new TrackController(S<ITrackRepository>(), S<ITrackMapper>());
			var conference = new Conference {Id = Guid.NewGuid(), Key = "foo"};
			ViewResult result = controller.New(conference);
			result.ViewName.ShouldEqual("Edit");
			result.ViewData.Model.ShouldEqual(new TrackForm {ConferenceId = conference.Id, ConferenceKey = "foo"});
		}

		[Test]
		public void Delete_should_delete_a_track_and_render_index()
		{
			var conference = new Conference {Key = "foo"};
			var track = new Track {Conference = conference};
			var repository = S<ITrackRepository>();
			var controller = new TrackController(repository, S<ITrackMapper>());

			RedirectToRouteResult result = controller.Delete(track);

			repository.AssertWasCalled(x => x.Delete(track));
			result.RedirectsTo<TrackController>(x => x.Index(null)).ShouldBeTrue();
		}
	}
}