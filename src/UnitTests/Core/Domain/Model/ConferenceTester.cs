using System;
using CodeCampServer.Core.Domain.Model;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Tarantino.Core.Commons.Model;
using Tarantino.UnitTests.Core.Commons.Model;

namespace CodeCampServer.UnitTests.Core.Domain.Model
{
	[TestFixture]
	public class ConferenceTester : PersistentObjectTester
	{
		[Test]
		public void Should_get_an_attendee_by_id()
		{
			var attendee = new Attendee {Id = Guid.NewGuid()};
			var attendee1 = new Attendee {Id = Guid.NewGuid()};
			var attendee2 = new Attendee {Id = Guid.NewGuid()};

			var conference = new Conference();
			conference.AddAttendee(attendee);
			conference.AddAttendee(attendee1);
			conference.AddAttendee(attendee2);

			Attendee attendee3 = conference.GetAttendee(attendee1.Id);

			attendee3.ShouldEqual(attendee1);
		}

		[Test]
		public void Should_report_having_an_attendee()
		{
			var attendee1 = new Attendee {Id = Guid.NewGuid()};
			var attendee2 = new Attendee {Id = Guid.NewGuid()};

			var conference = new Conference();
			conference.AddAttendee(attendee1);

			conference.IsAttending(attendee2.Id).ShouldBeFalse();
			conference.IsAttending(attendee1.Id).ShouldBeTrue();
		}

		[Test]
		public void Should_confirm_duplicate_attendee_email_address()
		{
			var attendee = new Attendee {EmailAddress = "foo"};
			var attendee1 = new Attendee {EmailAddress = "bar"};

			var conference = new Conference();
			conference.AddAttendee(attendee);
			conference.AddAttendee(attendee1);

			conference.IsAttending("foo").ShouldBeTrue();
			conference.IsAttending("baz").ShouldBeFalse();
		}

		protected override PersistentObject CreatePersisentObject()
		{
			return new Conference();
		}
	}
}