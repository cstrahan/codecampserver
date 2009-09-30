using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
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
            var form = S<UserGroupInput>();
            form.Id = Guid.Empty;
            form.Key = "key";
            form.Name = "name";
            form.Region = "region";
            form.GoogleAnalysticsCode = "qwer234234";
            form.HomepageHTML = "<h1>hello world</h1>";
            form.City = "austin";
            form.Country = "USA";
            Guid newGuid = Guid.NewGuid();
            form.Users = new[] {new UserSelectorInput() {Id = newGuid},};
            var userRepository = S<IUserRepository>();
            userRepository.Stub(repository => repository.GetAll()).Return(new[] {new User(){Id=newGuid}, new User()});
            var userGroupRepository = M<IUserGroupRepository>();
            userGroupRepository.Stub(x => x.GetById(form.Id)).Return(null);

            var mapper = new UserGroupMapper(userGroupRepository,userRepository);

            UserGroup mapped = mapper.Map(form);

            mapped.Key.ShouldEqual("key");
            mapped.Name.ShouldEqual("name");
            mapped.Region.ShouldEqual("region");
            mapped.GoogleAnalysticsCode.ShouldEqual("qwer234234");
            mapped.HomepageHTML.ShouldEqual("<h1>hello world</h1>");
            mapped.City.ShouldEqual("austin");
            mapped.Country.ShouldEqual("USA");
            mapped.GetUsers().Length.ShouldEqual(1);
            mapped.GetUsers()[0].Id.ShouldEqual(newGuid);
        }

        [Test]
        public void Should_map_existing_conference()
        {
            var form = S<UserGroupInput>();
            form.Id = Guid.NewGuid();
            form.Key = "key";
            form.Name = "name";
            form.Region = "region";
            form.GoogleAnalysticsCode = "qwer234234";
            form.HomepageHTML = "<h1>hello world</h1>";
            form.City = "austin";
            form.Country = "USA";
            form.Users = new[] {new UserSelectorInput() {}};
            
            
            var userGroup = new UserGroup();

            var repository = S<IUserGroupRepository>();
            repository.Stub(x => x.GetById(form.Id)).Return(userGroup);
            
            Guid newGuid = Guid.NewGuid();
            form.Users = new[] { new UserSelectorInput() { Id = newGuid }, };
            var userRepository = S<IUserRepository>();
            userRepository.Stub(r => r.GetAll()).Return(new[] { new User() { Id = newGuid }, new User() });
            
            var mapper = new UserGroupMapper(repository, userRepository);

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