using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Binders;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Helpers
{
    [TestFixture]
    public class UserGroupModelBinderTester:TestBase
    {
        [Test]
        public void The_binder_should_find_a_usergroup_by_its_key()
        {
            var userGroupRepository = S<IUserGroupRepository>();
            userGroupRepository.Stub(repository => repository.GetByKey("")).Return(null);
            var userGroup1 = new UserGroup();
            userGroupRepository.Stub(repository => repository.GetDefaultUserGroup()).Return(userGroup1);
            
            var modelBinder = new UserGroupModelBinder(userGroupRepository);
            UserGroup usergroup = (UserGroup) modelBinder.BindModel(new ControllerContext(), CreateBindingContext(""));
            
            usergroup.ShouldEqual(userGroup1);
        }

        [Test]
        public void The_binder_should_find_a_usergroup_by_its_key1()
        {
            var userGroupRepository = S<IUserGroupRepository>();
            userGroupRepository.Stub(repository => repository.GetByKey("foo")).Return(null);
            var userGroup1 = new UserGroup();
            userGroupRepository.Stub(repository => repository.GetDefaultUserGroup()).Return(userGroup1);
            
            var modelBinder = new UserGroupModelBinder(userGroupRepository);
            UserGroup usergroup = (UserGroup)modelBinder.BindModel(new ControllerContext(), CreateBindingContext("foo"));
            
            usergroup.ShouldEqual(userGroup1);
        }

        private ModelBindingContext CreateBindingContext(string value)
        {
            return new ModelBindingContext()
                       {
                           ModelName = "usergroupkey",
                           ModelType = typeof(UserGroup),
                           ValueProvider = CreateValueProvider(value, "usergroupkey")
                       };
        }

        [Test]
        public void The_binder_should_find_a_usergroup_by_its_key2()
        {
            var userGroupRepository = S<IUserGroupRepository>();
            var userGroup1 = new UserGroup(); 
            userGroupRepository.Stub(repository => repository.GetByKey("foo")).Return(userGroup1);
            
            
            var modelBinder = new UserGroupModelBinder(userGroupRepository);
            UserGroup usergroup = (UserGroup)modelBinder.BindModel(new ControllerContext(), CreateBindingContext("foo"));
            usergroup.ShouldEqual(userGroup1);
        }

        private Dictionary<string, ValueProviderResult> CreateValueProvider(string AttenptedValue, string key)
        {
            var dictionary = new Dictionary<string, ValueProviderResult>();
            dictionary.Add(key,new ValueProviderResult("",AttenptedValue,null));
            return dictionary;
        }
    }
}
