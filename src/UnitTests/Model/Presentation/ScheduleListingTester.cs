using System;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Model.Presentation
{
	[TestFixture]
	public class ScheduleListingTester
	{
		[Test]
		public void ShouldGetSessionListingForTrackListing()
		{
            Conference conference = new Conference();
            conference.Key = "key";
            conference.Name = "name";
            conference.StartDate = new DateTime(2000, 1, 1);

            Track dotNetTrack = new Track(".NET");
            Track webTrack = new Track("Web");
            TrackListing dotNetTrackListing = new TrackListing(dotNetTrack);
            TrackListing webTrackListing = new TrackListing(webTrack);

            TimeSlot timeSlot = new TimeSlot() { Conference = conference };
		    Session dotNetSession = new Session(conference, new Person(), "Session 1", "Session 1 abstract", dotNetTrack);
		    Session webSession = new Session(conference, new Person(), "Session 2", "Session 2 abstract", webTrack);
            timeSlot.AddSession(dotNetSession);
            timeSlot.AddSession(webSession);

            ScheduleListing scheduleListing = new ScheduleListing(timeSlot);
		    SessionListing[] sessionListings = scheduleListing.Sessions;

            Assert.That(scheduleListing[dotNetTrackListing], Is.EqualTo(sessionListings[0]));
            Assert.That(scheduleListing[webTrackListing], Is.EqualTo(sessionListings[1]));
		}
	}
}