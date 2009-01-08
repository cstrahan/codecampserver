using System;
using CodeCampServer.Core.Domain.Model;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Tarantino.Core.Commons.Model;
using Tarantino.UnitTests.Core.Commons.Model;

namespace CodeCampServer.UnitTests.Core.Domain.Model
{
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

		protected override PersistentObject CreatePersisentObject()
		{
			return new Conference();
		}
	}
}