using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Core.Services.Impl
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly ICryptographer _cryptographer;

		public AuthenticationService(ICryptographer cryptographer)
		{
			_cryptographer = cryptographer;
		}

		public bool PasswordMatches(User user, string password)
		{
			var passwordHash = _cryptographer.GetPasswordHash(password, user.PasswordSalt);
			return passwordHash.Equals(user.PasswordHash);
		}
	}
}