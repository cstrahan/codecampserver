using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CodeCampServer.Model.Security;
using StructureMap;
using CodeCampServer.Model;

namespace CodeCampServer.Website.Security
{
    [Pluggable(Keys.DEFAULT)]
    public class AuthorizationService : IAuthorizationService
    {
        private const string AdminRole = "Admin";

        #region IAuthorizationService Members

        public bool IsAdministrator
        {
            get
            {
                return HttpContext.Current.User.IsInRole(AdminRole);
            }           
        }

        #endregion
    }
}
