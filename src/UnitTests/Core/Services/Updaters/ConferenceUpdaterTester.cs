using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.Core.Services.Updaters.Impl;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class ConferenceUpdaterTester : TestBase
	{
		[Test]
		public void Should_add_new_conference()
		{
			var message = S<IConferenceMessage>();
			message.Id = Guid.Empty;
			message.Key = "key";
			message.Description = "desc";
			message.LocationName = "location";
			message.Name = "name";
			message.PhoneNumber = "phone";
			message.PostalCode = "postal";
			message.Region = "region";
			message.StartDate = "01/01/2008";
			message.EndDate = "01/02/2008";

			var repository = M<IConferenceRepository>();
			repository.Stub(x => x.GetById(message.Id)).Return(null);

			var updater = new ConferenceUpdater(repository);

			UpdateResult<Conference, IConferenceMessage> result = updater.UpdateFromMessage(message);
			Conference conference = result.Model;
			conference.Key.ShouldEqual("key");
			conference.Description.ShouldEqual("desc");
			conference.LocationName.ShouldEqual("location");
			conference.Name.ShouldEqual("name");
			conference.PhoneNumber.ShouldEqual("phone");
			conference.PostalCode.ShouldEqual("postal");
			conference.Region.ShouldEqual("region");
			conference.StartDate.ShouldEqual(new DateTime(2008, 1, 1));
			conference.EndDate.ShouldEqual(new DateTime(2008, 1, 2));
			result.Successful.ShouldBeTrue();
		}

		[Test]
		public void Should_update_existing_conference()
		{
			var conference = new Conference
			                 	{
			                 		Name = "sdf",
			                 		Description = "description",
			                 		StartDate = new DateTime(2008, 12, 2),
			                 		EndDate = new DateTime(2008, 12, 3),
			                 		LocationName = "St Edwards Professional Education Center",
			                 		Address = "12343 Research Blvd",
			                 		City = "Austin",
			                 		Region = "Tx",
			                 		PostalCode = "78234",
			                 		PhoneNumber = "512-555-1234"
			                 	};

			var message = S<IConferenceMessage>();
			message.Id = Guid.Empty;
			message.Key = "key";
			message.Description = "desc";
			message.LocationName = "location";
			message.Name = "name";
			message.PhoneNumber = "phone";
			message.PostalCode = "postal";
			message.Region = "region";
			message.StartDate = "01/01/2008";
			message.EndDate = "01/02/2008";

			var repository = S<IConferenceRepository>();
			repository.Stub(x => x.GetById(message.Id)).Return(conference);
			var updater = new ConferenceUpdater(repository);

			UpdateResult<Conference, IConferenceMessage> result = updater.UpdateFromMessage(message);

			result.Successful.ShouldBeTrue();
			result.Model.ShouldEqual(conference);

			conference.Key.ShouldEqual("key");
			conference.Description.ShouldEqual("desc");
			conference.LocationName.ShouldEqual("location");
			conference.Name.ShouldEqual("name");
			conference.PhoneNumber.ShouldEqual("phone");
			conference.PostalCode.ShouldEqual("postal");
			conference.Region.ShouldEqual("region");
			conference.StartDate.ShouldEqual(new DateTime(2008, 1, 1));
			conference.EndDate.ShouldEqual(new DateTime(2008, 1, 2));
			result.Successful.ShouldBeTrue();
		}

		[Test]
		public void Should_not_add_new_speaker_if_key_already_exists()
		{
			var message = S<IConferenceMessage>();
			message.Id = Guid.NewGuid();
			message.Key = "Key";

			var repository = M<IConferenceRepository>();
			var conference = new Conference();
			repository.Stub(s => s.GetByKey("Key")).Return(conference);

			IConferenceUpdater updater = new ConferenceUpdater(repository);

			UpdateResult<Conference, IConferenceMessage> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeFalse();

			CollectionAssert.Contains(updateResult.GetMessages(x => x.Key), "This conference key already exists");
		}
	}
}