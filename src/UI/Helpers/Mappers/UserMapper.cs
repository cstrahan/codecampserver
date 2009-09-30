using System;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class UserMapper : AutoInputMapper<User, UserInput>, IUserMapper
	{
		private readonly ICryptographer _cryptographer;

		public UserMapper(IUserRepository repository, ICryptographer cryptographer) : base(repository)
		{
			_cryptographer = cryptographer;
		}

		protected override Guid GetIdFromMessage(UserInput input)
		{
			return input.Id;
		}

		protected override void MapToModel(UserInput input, User model)
		{
			model.Id = input.Id;
			model.Name = input.Name;
			model.EmailAddress = input.EmailAddress;
			model.PasswordSalt = _cryptographer.CreateSalt();
			model.PasswordHash = _cryptographer.GetPasswordHash(input.Password,
			                                                    model.PasswordSalt);
			model.Username = input.Username;
		}

	    public UserInput[] Map(User[] model)
	    {
	        return model.Select(user => Map(user)).ToArray();
	    }

	    public User[] Map(UserInput[] message)
	    {
	        return message.Select(form => Map(form)).ToArray();
	    }
	}
}