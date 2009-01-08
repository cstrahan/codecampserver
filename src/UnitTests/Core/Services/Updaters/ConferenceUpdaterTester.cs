using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class ConferenceUpdaterTester : TestBase
	{
//		[Test]
//		public void Should_add_new_conference()
//		{
//			var message = S<IConferenceMessage>();
//			message.Id = Guid.Empty;
//			message.Key = "key";
//			message.LocationName = "location";
//			message.Name = "name";
//			message.PhoneNumber = "phone";
//			message.PostalCode = "postal";
//			message.Region = "region";
//			message.StartDate = "01/01/2008";
//
//			var repository = M<IConferenceRepository>();
//			repository.Stub(x => x.GetById(message.Id)).Return(null);
//
//			var updater = new AttendeeUpdater(repository);
//
//			UpdateResult<Conference, IConferenceMessage> result = updater.UpdateFromMessage(message);
//			var conference = result.Model;
//			conference.Key.ShouldEqual("Key");
//			conference.LocationName.ShouldEqual("location");
//			conference.Name.ShouldEqual("name");
//			conference.PhoneNumber.ShouldEqual("phone");
//			conference.PostalCode.ShouldEqual("postal");
//			conference.Region.ShouldEqual("region");
//			conference.StartDate.ShouldEqual(new DateTime(2008, 1, 1));
//			result.Successful.ShouldBeTrue();
//		}
//
//		[Test]
//		public void Should_update_existing_conference()
//		{
//		}
	}
}