using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Rhino.Mocks;
//using CodeCampServer.UI.Helpers.Mappers;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class SponsorControllerTester : ControllerTester
	{
		[Test]
		public void Should_list_the_sponors_for_a_user_group()
		{
			var repository = S<IUserGroupRepository>();
			repository.Stub(groupRepository => groupRepository.GetById(Guid.NewGuid())).IgnoreArguments().Return(new UserGroup());
			var controller = new SponsorController(repository, PermisiveSecurityContext());

			controller.Index(new UserGroup())
				.AssertViewRendered()
				.ForView(ViewNames.Default)
				.ModelShouldBe<Sponsor[]>()
				.AutoMappedModelShouldBe<SponsorInput[]>()
				;
		}


		[Test]
		public void Should_edit_an_existing_sponsor()
		{
			var controller = new SponsorController(S<IUserGroupRepository>(), PermisiveSecurityContext());

			var userGroup = new UserGroup();
			userGroup.Add(new Sponsor {Id = default(int)});

			controller.Edit(userGroup, new Sponsor())
				.AssertViewRendered()
				.ForView(ViewNames.Default)
				.ModelShouldBe<Sponsor>()
				.AutoMappedModelShouldBe<SponsorInput>()
				;
		}

		[Test]
		public void Should_save_a_new_sponsor_in_the_Save_action()
		{
			var userGroup = new UserGroup();

			var input = new SponsorInput();


			var controller = new SponsorController(null, PermisiveSecurityContext());

			var result = (CommandResult) controller.Edit(input, null);

			result.Success.AssertResultIs<RedirectToReturnUrlResult>();
		}

		[Test]
		public void Should_delete_a_sponsor_from_the_delete_action()
		{
			var repository = S<IUserGroupRepository>();
			var controller = new SponsorController(repository, PermisiveSecurityContext());

			var userGroup = new UserGroup();

			controller.Delete(userGroup, new Sponsor())
				.AssertActionRedirect()
				.ToAction<SponsorController>(c => c.Index(null));

			repository.AssertWasCalled(x => x.Save(userGroup));
		}
	}
}