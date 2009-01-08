using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class ConferenceControllerTester : TestControllerBase
	{
		[Test]
		public void
			When_a_conference_does_not_exist_Edit_should_redirect_to_the_index_with_a_message
			()
		{
			var repository = S<IConferenceRepository>();
			repository.Stub(repo => repo.GetAll()).Return(new Conference[0]);

			var controller = new ConferenceController(repository);

			ActionResult result = controller.Edit(Guid.Empty);
			result.AssertActionRedirect().ToAction<ConferenceController>(e => e.Index());
			controller.TempData["Message"].ShouldEqual(
				"Conference has been deleted.");
		}

		[Test]
		public void
			When_a_conference_does_not_exist_Index_action_should_redirect_to_new_when_conference_does_not_exist
			()
		{
			var repository = S<IConferenceRepository>();
			repository.Stub(repo => repo.GetAll()).Return(new Conference[0]);

			var controller = new ConferenceController(repository);

			ActionResult result = controller.Index();

			result.AssertActionRedirect().ToAction<ConferenceController>(a => a.New());
		}

		private ConferenceForm CreateConferenceForm(Guid id)
		{
			return new ConferenceForm
			       	{
			       		Id = id,
			       		Name = "Austin Code Camp",
			       		Description = "This is a code camp!",
			       		StartDate = "12/2/2008",
			       		EndDate = "12/3/2008",
			       		LocationName = "St Edwards Professional Education Center",
			       		Address = "1234 Main St",
			       		City = "Austin",
			       		Region = "Texas",
			       		PostalCode = "78787",
			       		PhoneNumber = "512-555-1234",
			       		Key = "AustinCodeCamp2008"
			       	};
		}

		[Test]
		public void When_a_conference_exists_Save_should_a_valid_user()
		{
			var conference = new Conference
			                 	{Name = "Austin Code Camp", Id = Guid.NewGuid()};
			var repository = S<IConferenceRepository>();
			repository.Stub(repo => repo.GetAll()).Return(new[] {conference});

			var controller = new ConferenceController(repository);

			ConferenceForm form = CreateConferenceForm(conference.Id);

			repository.Stub(c => c.GetById(conference.Id)).Return(conference);

			ActionResult result = controller.Save(form);

			result
				.AssertActionRedirect()
				.ToAction<ConferenceController>(a => a.Index());

			conference.Name.ShouldEqual("Austin Code Camp");
			conference.Description.ShouldEqual("This is a code camp!");
			conference.StartDate.ShouldEqual(DateTime.Parse("12/2/2008"));
			conference.EndDate.ShouldEqual(DateTime.Parse("12/3/2008"));
			conference.LocationName.ShouldEqual(
				"St Edwards Professional Education Center");
			conference.Address.ShouldEqual("1234 Main St");
			conference.City.ShouldEqual("Austin");
			conference.Region.ShouldEqual("Texas");
			conference.PostalCode.ShouldEqual("78787");
			conference.PhoneNumber.ShouldEqual("512-555-1234");
			conference.Key.ShouldEqual("AustinCodeCamp2008");
			repository.AssertWasCalled(r => r.Save(conference));
		}
	}
}