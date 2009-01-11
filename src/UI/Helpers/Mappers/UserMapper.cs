using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class UserMapper : FormMapper<User, UserForm>, IUserMapper
	{
		private readonly ICryptographer _cryptographer;

		public UserMapper(IUserRepository repository, ICryptographer cryptographer) : base(repository)
		{
			_cryptographer = cryptographer;
		}

		protected override Guid GetIdFromMessage(UserForm form)
		{
			return form.Id;
		}

		protected override void MapToModel(UserForm form, User model)
		{
			model.Id = form.Id;
			model.Name = form.Name;
			model.EmailAddress = form.EmailAddress;
			model.PasswordSalt = _cryptographer.CreateSalt();
			model.PasswordHash = _cryptographer.GetPasswordHash(form.Password,
			                                                    model.PasswordSalt);
			model.Username = form.Username;
		}
	}
}