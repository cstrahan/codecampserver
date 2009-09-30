using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class ConferenceMapperTester : TestBase
	{
		[Test]
		public void Should_map_new_conference()
		{
			var form = S<ConferenceInput>();
			form.Id = Guid.Empty;
			form.Key = "key";
			form.Description = "desc";
			form.LocationName = "location";
			form.Name = "name";
			form.PhoneNumber = "phone";
			form.PostalCode = "postal";
			form.Region = "region";
			form.StartDate = new DateTime(2008, 1, 1);//.ToShortDateString();
			form.EndDate = new DateTime(2008, 1, 2);//.ToShortDateString();
			form.TimeZone = "CST";
			form.HtmlContent = "<h1>this is some html fragments</h1>";
			form.LocationUrl = "http://foo";
			var repository = M<IConferenceRepository>();
			repository.Stub(x => x.GetById(form.Id)).Return(null);

			var mapper = new ConferenceMapper(repository, S<IUserGroupRepository>());

			Conference mapped = mapper.Map(form);

			mapped.LocationUrl.ShouldEqual("http://foo");
			mapped.Key.ShouldEqual("key");
			mapped.Description.ShouldEqual("desc");
			mapped.LocationName.ShouldEqual("location");
			mapped.Name.ShouldEqual("name");
			mapped.PhoneNumber.ShouldEqual("phone");
			mapped.PostalCode.ShouldEqual("postal");
			mapped.Region.ShouldEqual("region");
			mapped.StartDate.ShouldEqual(new DateTime(2008, 1, 1));
			mapped.EndDate.ShouldEqual(new DateTime(2008, 1, 2));
			mapped.TimeZone.ShouldEqual("CST");
			mapped.HtmlContent.ShouldEqual("<h1>this is some html fragments</h1>");
		}

		[Test]
		public void Should_map_existing_conference()
		{
			var form = S<ConferenceInput>();
			form.Id = Guid.NewGuid();
			form.Key = "key";
			form.Description = "desc";
			form.LocationName = "location";
			form.Name = "name";
			form.PhoneNumber = "phone";
			form.PostalCode = "postal";
			form.Region = "region";
			form.StartDate = new DateTime(2008, 1, 1);//.ToShortDateString();
			form.EndDate = new DateTime(2008, 1, 2);//.ToShortDateString();
			form.HasRegistration = true;

			var repository = S<IConferenceRepository>();
			var conference = new Conference();
			repository.Stub(x => x.GetById(form.Id)).Return(conference);
			var mapper = new ConferenceMapper(repository, S<IUserGroupRepository>());

			Conference mapped = mapper.Map(form);
			conference.ShouldEqual(mapped);
			conference.Key.ShouldEqual("key");
			conference.Description.ShouldEqual("desc");
			conference.LocationName.ShouldEqual("location");
			conference.Name.ShouldEqual("name");
			conference.PhoneNumber.ShouldEqual("phone");
			conference.PostalCode.ShouldEqual("postal");
			conference.Region.ShouldEqual("region");
			conference.StartDate.ShouldEqual(new DateTime(2008, 1, 1));
			conference.EndDate.ShouldEqual(new DateTime(2008, 1, 2));
			conference.HasRegistration.ShouldBeTrue();
		}

        //[Test]
        //public void Should_map_bad_date_to_null()
        //{
        //    var form = S<ConferenceInput>();
        //    form.Id = Guid.NewGuid();
        //    form.Key = "key";
        //    form.StartDate = "alskd";
        //    form.EndDate = "";
        //    form.HasRegistration = true;

        //    var repository = S<IConferenceRepository>();
        //    var conference = new Conference();
        //    repository.Stub(x => x.GetById(form.Id)).Return(conference);
        //    var mapper = new ConferenceMapper(repository, S<IUserGroupRepository>());

        //    Conference mapped = mapper.Map(form);
        //    conference.ShouldEqual(mapped);
        //    conference.Key.ShouldEqual("key");
        //    conference.StartDate.HasValue.ShouldBeFalse();
        //    conference.EndDate.HasValue.ShouldBeFalse();
        //}
	}
}