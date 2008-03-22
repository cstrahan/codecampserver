using System.Web;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Security
{
    public class AuthorizationService : IAuthorizationService
    {
        private const string AdminRole = "Admin";

        #region IAuthorizationService Members

        public bool IsAdministrator
        {
            get { return HttpContext.Current.User.IsInRole(AdminRole); }
        }

        #endregion
    }
}