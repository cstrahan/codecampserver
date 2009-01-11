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
	public class SpeakerMapperTester : TestBase
	{
		[Test]
		public void Should_save_new_speaker_from_form()
		{
			var form = S<SpeakerForm>();
			form.Id = Guid.Empty;
			form.Bio = "Bio";
			form.Company = "Company";
			form.EmailAddress = "EmailAddress";
			form.FirstName = "First";
			form.LastName = "Last";
			form.JobTitle = "Title";
			form.Key = "Key";
			form.WebsiteUrl = "Url";

			var repository = M<ISpeakerRepository>();
			repository.Stub(s => s.GetById(form.Id)).Return(null);

			ISpeakerMapper mapper = new SpeakerMapper(repository);

			Speaker mapped = mapper.Map(form);

			mapped.Bio.ShouldEqual("Bio");
			mapped.Company.ShouldEqual("Company");
			mapped.EmailAddress.ShouldEqual("EmailAddress");
			mapped.FirstName.ShouldEqual("First");
			mapped.LastName.ShouldEqual("Last");
			mapped.JobTitle.ShouldEqual("Title");
			mapped.Key.ShouldEqual("Key");
			mapped.WebsiteUrl.ShouldEqual("Url");
		}

		[Test]
		public void Should_map_existing_speaker_from_form()
		{
			var form = S<SpeakerForm>();
			form.Id = Guid.NewGuid();
			form.Bio = "Bio";
			form.Company = "Company";
			form.EmailAddress = "EmailAddress";
			form.FirstName = "First";
			form.LastName = "Last";
			form.JobTitle = "Title";
			form.Key = "Key";
			form.WebsiteUrl = "Url";

			var repository = M<ISpeakerRepository>();
			var speaker = new Speaker();
			repository.Stub(s => s.GetById(form.Id)).Return(speaker);

			ISpeakerMapper mapper = new SpeakerMapper(repository);

			Speaker mapped = mapper.Map(form);
			speaker.ShouldEqual(mapped);
			speaker.Bio.ShouldEqual("Bio");
			speaker.Company.ShouldEqual("Company");
			speaker.EmailAddress.ShouldEqual("EmailAddress");
			speaker.FirstName.ShouldEqual("First");
			speaker.LastName.ShouldEqual("Last");
			speaker.JobTitle.ShouldEqual("Title");
			speaker.Key.ShouldEqual("Key");
			speaker.WebsiteUrl.ShouldEqual("Url");
		}
	}
}