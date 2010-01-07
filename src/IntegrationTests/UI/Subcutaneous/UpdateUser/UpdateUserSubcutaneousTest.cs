using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.UI.Mappers;
using CodeCampServer.UI.Models.Input;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Tarantino.RulesEngine;

namespace CodeCampServer.IntegrationTests.UI.Subcutaneous.UpdateUser
{
	[TestFixture]
	public class UpdateUserSubcutaneousTest : SubcutaneousTest<UserInput>
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
			var cryptographer = GetInstance<ICryptographer>();
			var salt = cryptographer.CreateSalt();
			_user = new User()
			        	{
			                   		Name = "Joe Sub",
			                   		Username = "admin",
			                   		EmailAddress = "joe@user.com",
                                    PasswordHash = cryptographer.GetPasswordHash("password",salt),
                                    PasswordSalt = salt,
			        	};
			_anotherUser=	new User()			                          	{
			                          		Name = "Joe Two",
			                          		Username = "another",
			                          		EmailAddress = "two@user.com",
                                    PasswordHash = cryptographer.GetPasswordHash("password",salt),
                                    PasswordSalt = salt,
			                          	};

			PersistEntities(_user, _anotherUser);
		}

		private User _anotherUser;
		private User _user;

		[Test]
		public void Should_ensure_unique_username()
		{
			var input = new UserInput
			            	{
			            		Id = _user.Id,
			            		ConfirmPassword = "password",
			            		Password = "password",
			            		EmailAddress = "test@example.com",
			            		Name = "New Name",
			            		Username = "another"
			            	};

			ExecutionResult result = ProcessForm(input);

			result.ShouldNotBeSuccessful();
			result.ShouldHaveMessage<UserInput>(x => x.Username);
		}

		[Test]
		public void Should_require_confirm()
		{
			var input = new UserInput
			            	{
			            		Id = _user.Id,
			            		ConfirmPassword = null,
			            		Password = "password",
			            		EmailAddress = "test@example.com",
			            		Name = "Some Name",
			            		Username = "username"
			            	};

			ExecutionResult result = ProcessForm(input);

			result.ShouldNotBeSuccessful();
			result.ShouldHaveMessage<UserInput>(x => x.ConfirmPassword);
		}

		[Test]
		public void Should_require_confirm_matches_password()
		{
			var input = new UserInput
			            	{
			            		Id = _user.Id,
			            		ConfirmPassword = "somethingelse",
			            		Password = "password",
			            		EmailAddress = "test@example.com",
			            		Name = "Some Name",
			            		Username = "username"
			            	};

			ExecutionResult result = ProcessForm(input);

			result.ShouldNotBeSuccessful();
			result.ShouldHaveMessage<UserInput>(x => x.ConfirmPassword);
		}

		[Test]
		public void Should_require_email()
		{
			var input = new UserInput
			            	{
			            		Id = _user.Id,
			            		ConfirmPassword = "password",
			            		Password = "password",
			            		EmailAddress = null,
			            		Name = "Some Name",
			            		Username = "username"
			            	};

			ExecutionResult result = ProcessForm(input);

			result.ShouldNotBeSuccessful();
			result.ShouldHaveMessage<UserInput>(x => x.EmailAddress);
		}

		[Test]
		public void Should_require_name()
		{
			var input = new UserInput
			            	{
			            		Id = _user.Id,
			            		ConfirmPassword = "password",
			            		Password = "password",
			            		EmailAddress = "test@example.com",
			            		Name = null,
			            		Username = "username"
			            	};

			ExecutionResult result = ProcessForm(input);

			result.ShouldNotBeSuccessful();
			result.ShouldHaveMessage<UserInput>(x => x.Name);
		}

		[Test]
		public void Should_require_password()
		{
			var input = new UserInput
			            	{
			            		Id = _user.Id,
			            		ConfirmPassword = "password",
			            		Password = null,
			            		EmailAddress = "test@example.com",
			            		Name = "Some Name",
			            		Username = "username"
			            	};

			ExecutionResult result = ProcessForm(input);

			result.ShouldNotBeSuccessful();
			result.ShouldHaveMessage<UserInput>(x => x.Password);
		}

		[Test]
		public void Should_require_username()
		{
			var input = new UserInput
			            	{
			            		Id = _user.Id,
			            		ConfirmPassword = "password",
			            		Password = "password",
			            		EmailAddress = "test@example.com",
			            		Name = "New Name",
			            		Username = null
			            	};

			ExecutionResult result = ProcessForm(input);

			result.ShouldNotBeSuccessful();
			result.ShouldHaveMessage<UserInput>(x => x.Username);
		}

		[Test]
		public void Should_update_user()
		{
			var input = new UserInput
			            	{
			            		Id = _user.Id,
			            		ConfirmPassword = "password",
			            		Password = "password",
			            		EmailAddress = "test@example.com",
			            		Name = "New Name",
			            		Username = "updated"
			            	};

			ExecutionResult result = ProcessForm(input);

			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<User>().ShouldEqual(_user);

			Reload(ref _user);

			_user.ShouldEqual(_user);
			_user.Username.ShouldEqual(input.Username);
			_user.EmailAddress.ShouldEqual(input.EmailAddress);
			_user.Name.ShouldEqual(input.Name);
			GetInstance<IAuthenticationService>()
				.PasswordMatches(_user, "password").ShouldBeTrue();
		}
	}
}