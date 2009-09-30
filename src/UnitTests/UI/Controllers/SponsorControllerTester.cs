using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class SponsorControllerTester : SaveControllerTester
	{
		[Test]
		public void Should_list_the_sponors_for_a_user_group()
		{
			var controller = new SponsorController(S<IUserGroupRepository>(),S<IUserGroupSponsorMapper>(),PermisiveSecurityContext());

		    controller.Index(new UserGroup())
            
            .AssertViewRendered()
            .ForView(ViewNames.Default)
            .ModelShouldBe<SponsorInput[]>();
		}

	    [Test]
	    public void Should_add_a_sponsor_for_a_user_group()
	    {
            var controller = new SponsorController(S<IUserGroupRepository>(), S<IUserGroupSponsorMapper>(), PermisiveSecurityContext());

            controller.New(new UserGroup())

            .AssertViewRendered()
            .ForView(ViewNames.Edit)
            .ModelShouldBe<SponsorInput>();
	        
	    }

        [Test]
        public void Should_edit_an_existing_sponsor()
        {
            var controller = new SponsorController(S<IUserGroupRepository>(), S<IUserGroupSponsorMapper>(), PermisiveSecurityContext());

            var userGroup = new UserGroup();
            userGroup.Add(new Sponsor(){Id = Guid.Empty});
            
            controller.Edit(userGroup, Guid.Empty)

            .AssertViewRendered()
            .ForView(ViewNames.Default)
            .ModelShouldBe<SponsorInput>();

        }

	    [Test]
	    public void Should_save_a_new_sponsor_in_the_Save_action()
	    {
            var userGroup = new UserGroup();
	        
            var repository = S<IUserGroupRepository>();
            var controller = new SponsorController(repository, S<IUserGroupSponsorMapper>(), PermisiveSecurityContext());

	        controller.Save(userGroup, new SponsorInput())

	            .AssertActionRedirect()
	            .ToAction<SponsorController>(c => c.Index(userGroup));
	    }

	    [Test]
	    public void Should_delete_a_sponsor_from_the_delete_action()
	    {
            var repository = S<IUserGroupRepository>();
            var controller = new SponsorController(repository, S<IUserGroupSponsorMapper>(), PermisiveSecurityContext());

	        var userGroup = new UserGroup();

	        controller.Delete(userGroup, new Sponsor())
	            .AssertActionRedirect()
	            .ToAction<SponsorController>(c => c.Index(null));
            
            repository.AssertWasCalled(x => x.Save(userGroup));
	    }
	}
}