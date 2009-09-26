using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

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
			UserGroupForm form = MapToForm(userGroup);
			if (userGroup == null || userGroup.IsDefault())
			{
				return View("defaultIndex", form);
			}
			return View(form);
		}

		public ViewResult Events(UserGroup userGroup)
		{
			UserGroupForm form = MapToForm(userGroup);
			return View(form);
		}

		public ViewResult About(UserGroup userGroup)
		{
			UserGroupForm form = MapToForm(userGroup);
			return View(form);
		}

		private UserGroupForm MapToForm(UserGroup userGroup)
		{
			return _mapper.Map(userGroup);
		}
	}
}