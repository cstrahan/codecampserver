using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters.Impl;

namespace CodeCampServer.Core.Services.Updaters
{
	public class UserUpdater : ModelUpdater<User, IUserMessage>, IUserUpdater
	{
		private readonly IUserRepository _repository;
		private ICryptographer _cryptographer;

		public UserUpdater(IUserRepository repository, ICryptographer cryptographer)
		{
			_repository = repository;
			_cryptographer = cryptographer;
		}

		protected override IRepository<User> Repository
		{
			get { return _repository; }
		}

		protected override Guid GetIdFromMessage(IUserMessage message)
		{
			return message.Id;
		}

		protected override void UpdateModel(IUserMessage message, User model)
		{
			model.Name = message.Name;
			model.EmailAddress = message.EmailAddress;
			model.PasswordSalt = _cryptographer.CreateSalt();
			model.PasswordHash = _cryptographer.GetPasswordHash(message.Password,
			                                                    model.PasswordSalt);
			model.Username = message.Username;
		}

		protected override UpdateResult<User, IUserMessage> PreValidate(IUserMessage message)
		{
			if (SpeakerKeyAlreadyExists(message))
			{
				return new UpdateResult<User, IUserMessage>(false).WithMessage(x => x.Username, "This username already exists");
			}
			return base.PreValidate(message);
		}


		private bool SpeakerKeyAlreadyExists(IUserMessage message)
		{
			return _repository.GetByKey(message.Username) != null;
		}
	}
}