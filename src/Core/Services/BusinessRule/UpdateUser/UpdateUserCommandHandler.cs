using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateUser
{
	public class UpdateUserCommandHandler : Command<UpdateUserCommandMessage>
	{
		private readonly IUserRepository _repository;
		private readonly ICryptographer _cryptographer;

		public UpdateUserCommandHandler(IUserRepository meetingRepository, ICryptographer cryptographer)
		{
			_repository = meetingRepository;
			_cryptographer = cryptographer;
		}

		protected override ReturnValue Execute(UpdateUserCommandMessage commandMessage)
		{
			var user = _repository.GetById(commandMessage.Id)??new User();
			user.Name = commandMessage.Name;			
			user.EmailAddress = commandMessage.EmailAddress;
			user.PasswordSalt = _cryptographer.CreateSalt();
			user.PasswordHash = _cryptographer.GetPasswordHash(commandMessage.Password,
			                                                    user.PasswordSalt);
			user.Username = commandMessage.Username;
			
			_repository.Save(user);

			return new ReturnValue {Type = typeof (User), Value = user};
		}
	}
}