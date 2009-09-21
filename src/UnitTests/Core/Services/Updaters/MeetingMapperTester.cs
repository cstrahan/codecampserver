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
	public class MeetingMapperTester : TestBase
	{
		[Test]
		public void Should_map_new_meeting()
		{
			var form = S<MeetingForm>();
			form.Id = Guid.Empty;
			form.Key = "key";
			form.Description = "desc";
			form.LocationName = "location";
			form.Name = "name";
			form.PostalCode = "postal";
			form.Region = "region";
			form.StartDate = new DateTime(2008, 1, 1).ToShortDateString();
			form.EndDate = new DateTime(2008, 1, 2).ToShortDateString();
			form.TimeZone = "CST";
			form.LocationUrl = "http://foo";
			form.Topic = "topic";
			form.Summary = "summary";
			form.SpeakerName = "speakername";
			form.SpeakerBio = "speakerbio";
			form.SpeakerUrl = "speakerurl";

			var repository = M<IMeetingRepository>();
			repository.Stub(x => x.GetById(form.Id)).Return(null);

			var mapper = new MeetingMapper(repository, S<IUserGroupRepository>());

			Meeting mapped = mapper.Map(form);

			mapped.LocationUrl.ShouldEqual("http://foo");
			mapped.Key.ShouldEqual("key");
			mapped.Description.ShouldEqual("desc");
			mapped.LocationName.ShouldEqual("location");
			mapped.Name.ShouldEqual("name");
			mapped.PostalCode.ShouldEqual("postal");
			mapped.Region.ShouldEqual("region");
			mapped.StartDate.ShouldEqual(new DateTime(2008, 1, 1));
			mapped.EndDate.ShouldEqual(new DateTime(2008, 1, 2));
			mapped.TimeZone.ShouldEqual("CST");
			mapped.Topic.ShouldEqual("topic");
			mapped.Summary.ShouldEqual("summary");
			mapped.SpeakerName.ShouldEqual("speakername");
			mapped.SpeakerBio.ShouldEqual("speakerbio");
			mapped.SpeakerUrl.ShouldEqual("speakerurl");
		}

		[Test]
		public void Should_map_bad_date_to_null()
		{
						var form = S<MeetingForm>();
			form.Id = Guid.Empty;
			form.Key = "key";
			form.Description = "desc";
			form.LocationName = "location";
			form.Name = "name";
			form.PostalCode = "postal";
			form.Region = "region";
			form.StartDate = new DateTime(2008, 1, 1).ToShortDateString();
			form.EndDate = new DateTime(2008, 1, 2).ToShortDateString();
			form.TimeZone = "CST";
			form.LocationUrl = "http://foo";
			form.Topic = "topic";
			form.Summary = "summary";
			form.SpeakerName = "speakername";
			form.SpeakerBio = "speakerbio";
			form.SpeakerUrl = "speakerurl";

			var repository = S<IMeetingRepository>();
			var meeting = new Meeting();
			repository.Stub(x => x.GetById(form.Id)).Return(meeting);
			var mapper = new MeetingMapper(repository, S<IUserGroupRepository>());

			Meeting mapped = mapper.Map(form);

			mapped.LocationUrl.ShouldEqual("http://foo");
			mapped.Key.ShouldEqual("key");
			mapped.Description.ShouldEqual("desc");
			mapped.LocationName.ShouldEqual("location");
			mapped.Name.ShouldEqual("name");
			mapped.PostalCode.ShouldEqual("postal");
			mapped.Region.ShouldEqual("region");
			mapped.StartDate.ShouldEqual(new DateTime(2008, 1, 1));
			mapped.EndDate.ShouldEqual(new DateTime(2008, 1, 2));
			mapped.TimeZone.ShouldEqual("CST");
			mapped.Topic.ShouldEqual("topic");
			mapped.Summary.ShouldEqual("summary");
			mapped.SpeakerName.ShouldEqual("speakername");
			mapped.SpeakerBio.ShouldEqual("speakerbio");
			mapped.SpeakerUrl.ShouldEqual("speakerurl");

		}

		
	}
}