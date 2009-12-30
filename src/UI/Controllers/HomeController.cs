using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class HomeController : ConventionController
	{
		public ViewResult Index(UserGroup userGroup)
		{
			ViewResult result = AutoMappedView<UserGroupInput>(userGroup);
			if (userGroup == null || userGroup.IsDefault())
			{
				result.ViewName = "defaultIndex";
			}
			return result;
		}
        
		public ViewResult Events(UserGroup userGroup)
		{
			return AutoMappedView<UserGroupInput>(userGroup);
		}

		public ViewResult About(UserGroup userGroup)
		{
			return AutoMappedView<UserGroupInput>(userGroup);
		}
	}
}