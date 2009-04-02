using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI;
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
			var controller = new TrackController(repository, mapper, null, PermisiveSecurityContext());

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
			var controller = new TrackController(S<ITrackRepository>(), mapper, null, PermisiveSecurityContext());

			ViewResult edit = controller.Edit(track);

			edit.ViewName.ShouldEqual(ViewNames.Default);
			var form = ((TrackForm)controller.ViewData.Model);
			form.ShouldEqual(trackForm);
		}

        [Test]
        public void Edit_should_prevent_non_administrators_from_editing()
        {
            var track = new Track();

            var controller = new TrackController(null, null, null, RestrictiveSecurityContext());

            ViewResult edit = controller.Edit(track);

            edit.ForView(ViewPages.NotAuthorized);
        }

		[Test]
		public void Should_save_the_track()
		{
			var form = new TrackForm();
			var track = new Track();

			var mapper = S<ITrackMapper>();
			mapper.Stub(m => m.Map(form)).Return(track);

			var repository = S<ITrackRepository>();

			var controller = new TrackController(repository, mapper, null, PermisiveSecurityContext());
			var result = (RedirectToRouteResult)controller.Save(form,null,null);

			repository.AssertWasCalled(r => r.Save(track));
			result.AssertActionRedirect().ToAction<TrackController>(a => a.Index(null));
		}

        [Test]
        public void Save_should_prevent_non_administrators_from_saving_a_track()
        {
            var form = new TrackForm();

            var controller = new TrackController(null, null, null, RestrictiveSecurityContext());

            var result = controller.Save(form, null, null);

            result.AssertViewRendered().ForView(ViewPages.NotAuthorized);
        }


		[Test]
		public void New_should_but_a_new_track_form_on_model_and_render_edit_view()
		{
			var controller = new TrackController(S<ITrackRepository>(), S<ITrackMapper>(), null, PermisiveSecurityContext());
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
			var sessionsRepository = S<ISessionRepository>();
			sessionsRepository.Stub(r => r.GetAllForTrack(null)).IgnoreArguments().Return(new Session[0] );

			var controller = new TrackController(repository, S<ITrackMapper>(), sessionsRepository, PermisiveSecurityContext());

			var result = controller.Delete(track);

			repository.AssertWasCalled(x => x.Delete(track));
			result.AssertActionRedirect().RedirectsTo<TrackController>(x => x.Index(null)).ShouldBeTrue();
		}

        [Test]
        public void Delete_should_prevent_a_an_non_admin_from_deleting_a_Track()
        {
            var conference = new Conference { Key = "foo" };
            var track = new Track { Conference = conference };

            var controller = new TrackController(null, null , null,RestrictiveSecurityContext());

            var result = controller.Delete(track);
        
            result.AssertViewRendered().ForView(ViewPages.NotAuthorized);

        }

		[Test]
		public void Delete_should_set_a_warning_and_render_index_when_a_track_is_in_use_by_a_session()
		{
			var conference = new Conference { Key = "foo" };
			var track = new Track() { Conference = conference };
			var repository = S<ITrackRepository>();
			var sessionsRepository = S<ISessionRepository>();
			sessionsRepository.Stub(r => r.GetAllForTrack(null)).IgnoreArguments().Return(new Session[] { new Session() });

			var controller = new TrackController(repository, S<ITrackMapper>(), sessionsRepository, PermisiveSecurityContext());

			var result = controller.Delete(track);

			repository.AssertWasNotCalled(x => x.Delete(track));
			
			result
				.AssertActionRedirect()
				.ToAction<TrackController>(x => x.Index(null));


			controller.TempData.ContainsValue("Track cannot be deleted.").ShouldBeTrue();

		}
	}
}