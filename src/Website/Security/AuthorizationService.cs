using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Security
{
	public class AuthorizationService : IAuthorizationService
	{
		private const string AdminRole = "Administrator";
		private readonly IHttpContextProvider _httpContextProvider;


		public AuthorizationService(IHttpContextProvider httpContextProvider)
		{
			_httpContextProvider = httpContextProvider;
		}

		public bool IsAdministrator
		{
			get { return _httpContextProvider.GetCurrentHttpContext().User.IsInRole(AdminRole); }
		}
	}
}