using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models;
using MvcContrib;
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
            ViewData.Add<PageInfo>(new PageInfo {Title = userGroup.Name});
            return View(_mapper.Map(userGroup));
        }
    }
}