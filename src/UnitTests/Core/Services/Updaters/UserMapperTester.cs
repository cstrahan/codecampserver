using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class UserMapperTester : TestBase
	{
		[Test]
		public void Should_map_new_user_from_form()
		{
			var form = S<UserForm>();
			form.Id = Guid.Empty;
			form.Username = "username";
			form.Password = "password";
			form.EmailAddress = "email";
			form.Name = "name";

			var repository = S<IUserRepository>();
			repository.Stub(s => s.GetById(form.Id)).Return(null);
			var cryptographer = S<ICryptographer>();
			cryptographer.Stub(c => c.CreateSalt()).Return("salt");
			cryptographer.Stub(c => c.GetPasswordHash("password", "salt")).Return("hash");

			IUserMapper mapper = new UserMapper(repository, cryptographer);

			User mapped = mapper.Map(form);
			mapped.Username.ShouldEqual("username");
			mapped.PasswordSalt.ShouldEqual("salt");
			mapped.PasswordHash.ShouldEqual("hash");
			mapped.EmailAddress.ShouldEqual("email");
			mapped.Name.ShouldEqual("name");
		}

		[Test]
		public void Should_map_existing_user_from_form()
		{
			var form = S<UserForm>();
			form.Id = Guid.Empty;
			form.Username = "username";
			form.Password = "password";
			form.EmailAddress = "email";
			form.Name = "name";

			var repository = S<IUserRepository>();
			var user = new User();
			repository.Stub(s => s.GetById(form.Id)).Return(user);
			var cryptographer = S<ICryptographer>();
			cryptographer.Stub(c => c.CreateSalt()).Return("salt");
			cryptographer.Stub(c => c.GetPasswordHash("password", "salt")).Return("hash");

			IUserMapper mapper = new UserMapper(repository, cryptographer);

			User mapped = mapper.Map(form);
			user.ShouldEqual(mapped);
			user.ShouldBeTheSameAs(user);
			user.Username.ShouldEqual("username");
			user.PasswordSalt.ShouldEqual("salt");
			user.PasswordHash.ShouldEqual("hash");
			user.EmailAddress.ShouldEqual("email");
			user.Name.ShouldEqual("name");
		}
	}
}