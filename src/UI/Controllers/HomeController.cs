using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	[AdminUserCreatedFilter]
	public class HomeController : SmartController
	{
		private readonly IUserGroupMapper _mapper;

		public HomeController(IUserGroupMapper mapper)
		{
			_mapper = mapper;
		}

		public ViewResult Index(UserGroup userGroup)
		{
			UserGroupInput input = MapToForm(userGroup);
			if (userGroup == null || userGroup.IsDefault())
			{
				return View("defaultIndex", input);
			}
			return View(input);
		}

		public ViewResult Events(UserGroup userGroup)
		{
			UserGroupInput input = MapToForm(userGroup);
			return View(input);
		}

		public ViewResult About(UserGroup userGroup)
		{
			UserGroupInput input = MapToForm(userGroup);
			return View(input);
		}

		private UserGroupInput MapToForm(UserGroup userGroup)
		{
			return _mapper.Map(userGroup);
		}
	}
}