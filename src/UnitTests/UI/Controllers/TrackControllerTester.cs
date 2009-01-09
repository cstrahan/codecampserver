using System;
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

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class TrackControllerTester : TestControllerBase
	{
		[Test]
		public void Index_should_put_tracks_for_conference_in_viewdata()
		{
			var conference = new Conference();
			var repository = S<ITrackRepository>();
			var tracks = new[] {new Track()};
			repository.Stub(x => x.GetAllForConference(conference)).Return(tracks);
			var controller = new TrackController(repository, S<ITrackUpdater>());

			ViewResult result = controller.Index(conference);

			result.ViewData.Get<Track[]>().ShouldEqual(tracks);
			result.ViewName.ShouldEqual(ViewNames.Default);
		}

		[Test]
		public void Edit_should_but_track_in_viewdata()
		{
			var track = new Track();
			var controller = new TrackController(S<ITrackRepository>(), S<ITrackUpdater>());

			ViewResult edit = controller.Edit(track);

			edit.ViewData.Get<Track>().ShouldEqual(track);
			edit.ViewName.ShouldEqual(ViewNames.Default);
		}

		[Test]
		public void Save_test_a_valid_save()
		{
			var form = new TrackForm();
			var updater = M<ITrackUpdater>();
			updater.Stub(x => x.UpdateFromMessage(form)).Return(ModelUpdater<Track, ITrackMessage>.Success());
			var controller = new TrackController(S<ITrackRepository>(), updater);

			var result = (RedirectToRouteResult) controller.Save(form);

			result.RedirectsTo<TrackController>(x => x.Index(null)).ShouldBeTrue();
		}

		[Test]
		public void Save_test_an_invaliSSd_save()
		{
			var form = new TrackForm();
			var updater = M<ITrackUpdater>();
			updater.Stub(x => x.UpdateFromMessage(form)).Return(ModelUpdater<Track, ITrackMessage>.Fail().WithMessage(
			                                                    	x => x.Name, "Some Message"));
			var controller = new TrackController(S<ITrackRepository>(), updater);

			var result = (ViewResult) controller.Save(form);
			result.ViewData.ModelState.ContainsKey("Name").ShouldBeTrue();
			result.ViewName.ShouldEqual("Edit");
		}

		[Test]
		public void New_should_but_a_new_track_form_on_model_and_render_edit_view()
		{
			var controller = new TrackController(S<ITrackRepository>(), S<ITrackUpdater>());
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
			var controller = new TrackController(repository, S<ITrackUpdater>());

			RedirectToRouteResult result = controller.Delete(track);

			repository.AssertWasCalled(x => x.Delete(track));
			result.RedirectsTo<TrackController>(x => x.Index(null)).ShouldBeTrue();
		}
	}
}