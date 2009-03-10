using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;

namespace CodeCampServer.UI.Controllers
{
	[AdminUserCreatedFilterAttribute]
	[RequiresConferenceFilter]
	public class HomeController : SmartController
	{
	    private readonly IUserGroupMapper _mapper;

	    public HomeController(IUserGroupMapper mapper)
	    {
	        _mapper = mapper;
	    }

	    public ViewResult Index(UserGroup userGroup)
		{

	        return View(_mapper.Map(userGroup));
		}
	}
}