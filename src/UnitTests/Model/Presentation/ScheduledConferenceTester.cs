using System;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Model.Presentation
{
	[TestFixture]
	public class ScheduledConferenceTester
	{
		[Test]
		public void ShouldWrapProperties()
		{
			Conference conference = new Conference();
			conference.Key = "key";
			conference.Name = "name";
			conference.StartDate = new DateTime(2000, 1, 1);
			Schedule schedule = new Schedule(
				conference, new ClockStub(), null);
			
			Assert.That(schedule.Key, Is.EqualTo("key"));
			Assert.That(schedule.Name, Is.EqualTo("name"));
			Assert.That(schedule.StartDate, Is.EqualTo(new DateTime(2000, 1, 1)));
			Assert.That(schedule.Conference, Is.EqualTo(conference));
		}

		[Test]
		public void ShouldCalculateDaysUntilConferenceStart_10Days()
		{
			Conference conference = new Conference();
			conference.StartDate = new DateTime(2000, 1, 11);
			Schedule schedule = new Schedule(
				conference, new ClockStub(new DateTime(2000, 1, 1)), null);

			Assert.That(schedule.DaysUntilStart, Is.EqualTo(10));
		}

		[Test]
		public void ShouldCalculateDaysUntilConferenceStart_Minus10Days()
		{
			Conference conference = new Conference();
			conference.StartDate = new DateTime(1999, 12, 21);
			Schedule schedule = new Schedule(
				conference, new ClockStub(new DateTime(2000, 1, 1)), null);

			Assert.That(schedule.DaysUntilStart, Is.Null);
		}

		[Test]
		public void ShouldCalculateDaysUntilConferenceStart_NullStartDate()
		{
			Conference conference = new Conference();
			conference.StartDate = null;
			Schedule schedule = new Schedule(
				conference, new ClockStub(new DateTime(2000, 1, 1)), null);

			Assert.That(schedule.DaysUntilStart, Is.Null);
		}
	}
}