using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using CommandProcessor;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.RulesEngine;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class SponsorControllerTester : SaveControllerTester
	{
		[Test]
		public void Should_list_the_sponors_for_a_user_group()
		{
			var mapper = S<IUserGroupSponsorMapper>();
			mapper.Stub(sponsorMapper => sponsorMapper.Map((Sponsor[]) null)).IgnoreArguments().Return(new SponsorInput[0]);
			var repository = S<IUserGroupRepository>();
			repository.Stub(groupRepository => groupRepository.GetById(Guid.NewGuid())).IgnoreArguments().Return(new UserGroup());
			var controller = new SponsorController(repository,mapper,PermisiveSecurityContext(), null);

		    controller.Index(new UserGroup())
            
            .AssertViewRendered()
            .ForView(ViewNames.Default)
            .ModelShouldBe<SponsorInput[]>();
		}

	  

        [Test]
        public void Should_edit_an_existing_sponsor()
        {
        	var mapper = S<IUserGroupSponsorMapper>();
        	mapper.Stub(sponsorMapper => sponsorMapper.Map((Sponsor) null)).IgnoreArguments().Return(new SponsorInput());
        	var controller = new SponsorController(S<IUserGroupRepository>(), mapper, PermisiveSecurityContext(), null);

            var userGroup = new UserGroup();
            userGroup.Add(new Sponsor(){Id = Guid.Empty});
            
            controller.Edit(userGroup, new Sponsor())

            .AssertViewRendered()
            .ForView(ViewNames.Default)
            .ModelShouldBe<SponsorInput>();

        }

	    [Test]
	    public void Should_save_a_new_sponsor_in_the_Save_action()
	    {
            var userGroup = new UserGroup();

			var input = new SponsorInput();

	    	var engine = S<IRulesEngine>();	    	
	    	engine.Stub(rulesEngine => rulesEngine.Process(input)).Return(new ExecutionResult());

	    	var controller = new SponsorController(null, null, PermisiveSecurityContext(), engine);

	        controller.Edit(userGroup, input)

	            .AssertActionRedirect()
	            .ToAction<SponsorController>(c => c.Index(userGroup));
	    }

	    [Test]
	    public void Should_delete_a_sponsor_from_the_delete_action()
	    {
            var repository = S<IUserGroupRepository>();
            var controller = new SponsorController(repository, S<IUserGroupSponsorMapper>(), PermisiveSecurityContext(), null);

	        var userGroup = new UserGroup();

	        controller.Delete(userGroup, new Sponsor())
	            .AssertActionRedirect()
	            .ToAction<SponsorController>(c => c.Index(null));
            
            repository.AssertWasCalled(x => x.Save(userGroup));
	    }
	}
}