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
	public class UserGroupMapperTester : TestBase
	{
		[Test]
		public void Should_add_new_usergroup()
		{
			var form = S<UserGroupForm>();
			form.Id = Guid.Empty;
			form.Key = "key";			
			form.Name = "name";
			form.Region = "region";
		    form.GoogleAnalysticsCode = "qwer234234";
		    form.HomepageHTML = "<h1>hello world</h1>";
		    form.City = "austin";
		    form.Country = "USA";

			var repository = M<IUserGroupRepository>();
			repository.Stub(x => x.GetById(form.Id)).Return(null);

			var mapper = new UserGroupMapper(repository);

			UserGroup mapped = mapper.Map(form);

			mapped.Key.ShouldEqual("key");
			mapped.Name.ShouldEqual("name");
			mapped.Region.ShouldEqual("region");
            mapped.GoogleAnalysticsCode.ShouldEqual("qwer234234");
            mapped.HomepageHTML.ShouldEqual("<h1>hello world</h1>");
            mapped.City.ShouldEqual("austin");
            mapped.Country.ShouldEqual("USA");
		}

		[Test]
		public void Should_map_existing_conference()
		{
			var form = S<UserGroupForm>();
			form.Id = Guid.NewGuid();
            form.Key = "key";
            form.Name = "name";
            form.Region = "region";
            form.GoogleAnalysticsCode = "qwer234234";
            form.HomepageHTML = "<h1>hello world</h1>";
            form.City = "austin";
            form.Country = "USA";

			var repository = S<IUserGroupRepository>();
			var userGroup = new UserGroup();
			repository.Stub(x => x.GetById(form.Id)).Return(userGroup);
            var mapper = new UserGroupMapper(repository);

            UserGroup mapped = mapper.Map(form);

            mapped.Key.ShouldEqual("key");
            mapped.Name.ShouldEqual("name");
            mapped.Region.ShouldEqual("region");
            mapped.GoogleAnalysticsCode.ShouldEqual("qwer234234");
            mapped.HomepageHTML.ShouldEqual("<h1>hello world</h1>");
            mapped.City.ShouldEqual("austin");
            mapped.Country.ShouldEqual("USA");
        }
	}
}