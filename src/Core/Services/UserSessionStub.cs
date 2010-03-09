using System;
using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Core.Services
{
	public class UserSessionStub : IUserSession
	{
		private User _currentUser;

		public UserSessionStub(User currentUser)
		{
			_currentUser = currentUser;
		}

		public virtual User GetCurrentUser()
		{
			return _currentUser;
		}

		public void LogIn(User user)
		{
			_currentUser = user;
		}

		public void LogOut()
		{
			throw new NotImplementedException();
		}
	}
}