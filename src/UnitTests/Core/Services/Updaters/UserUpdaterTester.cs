using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services;
using CodeCampServer.Core.Services.Updaters;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class UserUpdaterTester : TestBase
	{
		[Test]
		public void Should_save_new_user_from_message()
		{
			var message = S<IUserMessage>();
			message.Id = Guid.Empty;
			message.Username = "username";
			message.Password = "password";
			message.EmailAddress = "email";
			message.Name = "name";

			var repository = S<IUserRepository>();
			repository.Stub(s => s.GetById(message.Id)).Return(null);
			var cryptographer = S<ICryptographer>();
			cryptographer.Stub(c => c.CreateSalt()).Return("salt");
			cryptographer.Stub(c => c.GetPasswordHash("password", "salt")).Return("hash");

			IUserUpdater updater = new UserUpdater(repository, cryptographer);

			UpdateResult<User, IUserMessage> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeTrue();
			User user = updateResult.Model;
			user.Username.ShouldEqual("username");
			user.PasswordSalt.ShouldEqual("salt");
			user.PasswordHash.ShouldEqual("hash");
			user.EmailAddress.ShouldEqual("email");
			user.Name.ShouldEqual("name");
			repository.AssertWasCalled(s => s.Save(user));
		}

		[Test]
		public void Should_update_existing_user_from_message()
		{
			var message = S<IUserMessage>();
			message.Id = Guid.Empty;
			message.Username = "username";
			message.Password = "password";
			message.EmailAddress = "email";
			message.Name = "name";

			var repository = S<IUserRepository>();
			var existingUser = new User();
			repository.Stub(s => s.GetById(message.Id)).Return(existingUser);
			var cryptographer = S<ICryptographer>();
			cryptographer.Stub(c => c.CreateSalt()).Return("salt");
			cryptographer.Stub(c => c.GetPasswordHash("password", "salt")).Return("hash");

			IUserUpdater updater = new UserUpdater(repository, cryptographer);

			UpdateResult<User, IUserMessage> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeTrue();
			User user = updateResult.Model;
			user.ShouldBeTheSameAs(existingUser);
			user.Username.ShouldEqual("username");
			user.PasswordSalt.ShouldEqual("salt");
			user.PasswordHash.ShouldEqual("hash");
			user.EmailAddress.ShouldEqual("email");
			user.Name.ShouldEqual("name");
			repository.AssertWasCalled(s => s.Save(user));
		}

		[Test]
		public void Should_not_add_new_user_if_username_already_exists()
		{
			var message = S<IUserMessage>();
			message.Id = Guid.NewGuid();
			message.Username = "username1";

			var repository = M<IUserRepository>();
			var user = new User();
			repository.Stub(s => s.GetByKey("username1")).Return(user);

			IUserUpdater updater = new UserUpdater(repository, S<ICryptographer>());

			UpdateResult<User, IUserMessage> updateResult = updater.UpdateFromMessage(message);

			updateResult.Successful.ShouldBeFalse();

			CollectionAssert.Contains(updateResult.GetMessages(x => x.Username), "This username already exists");
		}
	}
}