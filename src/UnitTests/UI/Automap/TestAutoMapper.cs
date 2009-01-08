using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models;
using CodeCampServer.UI.Models.AutoMap;
using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Automap
{
	[TestFixture]
	public class TestAutoMapper
	{
		[Test]
		public void can_map_between_userForm_and_user()
		{
			AutoMapperConfiguration.Configure();
			User user = new User()
			            	{
			            		Username = "2asdfsad",
			            		PasswordHash = "pasdfsa",
			            		Id = Guid.NewGuid(),
			            		EmailAddress = "",
			            		Name = ""
			            	};
			UserForm form =
				(UserForm) AutoMapper.Map(user, typeof (User), typeof (UserForm));

			form.Id.ShouldEqual(user.Id);
			form.Password.ShouldBeEmpty();
		}
	}
}