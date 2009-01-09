using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
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
			var repository = M<ITrackRepository>();
			var tracks = new[] {new Track()};
			repository.Stub(x => x.GetAllForConference(conference)).Return(tracks);
			var controller = new TrackController(repository, M<ITrackUpdater>());

			var result = (ViewResult) controller.Index(conference);

			result.ViewData.Get<Track[]>().ShouldEqual(tracks);
			result.ViewName.ShouldEqual(ViewNames.Default);
		}
	}
}