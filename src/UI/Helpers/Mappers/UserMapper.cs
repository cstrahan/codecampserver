using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.AutoMap;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class UserMapper : ModelUpdater<User, UserForm>, IUserMapper
	{
		private readonly IUserRepository _repository;
		private readonly ICryptographer _cryptographer;

		public UserMapper(IUserRepository repository, ICryptographer cryptographer)
		{
			_repository = repository;
			_cryptographer = cryptographer;
		}

		protected override IRepository<User> Repository
		{
			get { return _repository; }
		}

		protected override Guid GetIdFromMessage(UserForm message)
		{
			return message.Id;
		}

		protected override void UpdateModel(UserForm message, User model)
		{
			model.Id = message.Id;
			model.Name = message.Name;
			model.EmailAddress = message.EmailAddress;
			model.PasswordSalt = _cryptographer.CreateSalt();
			model.PasswordHash = _cryptographer.GetPasswordHash(message.Password,
			                                                    model.PasswordSalt);
			model.Username = message.Username;
		}

		protected override UpdateResult<User, UserForm> PreValidate(UserForm message)
		{
			if (UserAlreadyExists(message))
			{
				return new UpdateResult<User, UserForm>(false).WithMessage(x => x.Username, "This username already exists");
			}
			return base.PreValidate(message);
		}


		private bool UserAlreadyExists(UserForm message)
		{
			if(message.GetEditMode() == EditMode.Edit) return false;

			return _repository.GetByKey(message.Username) != null;
		}

		public User Map(UserForm sourceObject)
		{
			return UpdateFromMessage(sourceObject).Model;
		}

		public virtual UserForm Map(User sourceObject)
		{
			return AutoMapper.Map<User, UserForm>(sourceObject);
		}
	}
}