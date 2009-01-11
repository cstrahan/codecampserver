using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class SpeakerUpdaterTester : TestBase
	{
		[Test]
		public void Should_save_new_speaker_from_message()
		{
			var message = S<SpeakerForm>();
			message.Id = Guid.Empty;
			message.Bio = "Bio";
			message.Company = "Company";
			message.EmailAddress = "EmailAddress";
			message.FirstName = "First";
			message.LastName = "Last";
			message.JobTitle = "Title";
			message.Key = "Key";
			message.WebsiteUrl = "Url";

			var repository = M<ISpeakerRepository>();
			repository.Stub(s => s.GetById(message.Id)).Return(null);

			ISpeakerUpdater updater = new SpeakerUpdater(repository);

			UpdateResult<Speaker, SpeakerForm> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeTrue();
			Speaker speaker = updateResult.Model;
			speaker.Bio.ShouldEqual("Bio");
			speaker.Company.ShouldEqual("Company");
			speaker.EmailAddress.ShouldEqual("EmailAddress");
			speaker.FirstName.ShouldEqual("First");
			speaker.LastName.ShouldEqual("Last");
			speaker.JobTitle.ShouldEqual("Title");
			speaker.Key.ShouldEqual("Key");
			speaker.WebsiteUrl.ShouldEqual("Url");
			repository.AssertWasCalled(s => s.Save(speaker));
		}

		[Test]
		public void Should_update_existing_speaker_from_message()
		{
			var message = S<SpeakerForm>();
			message.Id = Guid.NewGuid();
			message.Bio = "Bio";
			message.Company = "Company";
			message.EmailAddress = "EmailAddress";
			message.FirstName = "First";
			message.LastName = "Last";
			message.JobTitle = "Title";
			message.Key = "Key";
			message.WebsiteUrl = "Url";

			var repository = M<ISpeakerRepository>();
			var speaker = new Speaker();
			repository.Stub(s => s.GetById(message.Id)).Return(speaker);

			ISpeakerUpdater updater = new SpeakerUpdater(repository);

			UpdateResult<Speaker, SpeakerForm> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeTrue();

			speaker.Bio.ShouldEqual("Bio");
			speaker.Company.ShouldEqual("Company");
			speaker.EmailAddress.ShouldEqual("EmailAddress");
			speaker.FirstName.ShouldEqual("First");
			speaker.LastName.ShouldEqual("Last");
			speaker.JobTitle.ShouldEqual("Title");
			speaker.Key.ShouldEqual("Key");
			speaker.WebsiteUrl.ShouldEqual("Url");
			repository.AssertWasCalled(s => s.Save(speaker));
		}

		[Test]
		public void Should_not_add_new_speaker_if_key_already_exists()
		{
			var message = S<SpeakerForm>();
			message.Id = Guid.NewGuid();
			message.Key = "Key";

			var repository = M<ISpeakerRepository>();
			var speaker = new Speaker();
			repository.Stub(s => s.GetByKey("Key")).Return(speaker);

			ISpeakerUpdater updater = new SpeakerUpdater(repository);

			UpdateResult<Speaker, SpeakerForm> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeFalse();

			CollectionAssert.Contains(updateResult.GetMessages(x => x.Key), "This speaker key already exists");
		}
	}
}