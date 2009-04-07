using System.Web.Mvc;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models;
using CodeCampServer.UI.Models.Forms;
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

        public ViewResult Index(UserGroup userGroup, IConferenceRepository _conferenceRepository)
        {
            Conference[] conferences = _conferenceRepository.GetFutureForUserGroup(userGroup);

            var conferenceForms =
                (ConferenceForm[]) Mapper.Map(conferences, typeof (Conference[]), typeof (ConferenceForm[]));

            ViewData.Add(conferenceForms);

            
            return View(_mapper.Map(userGroup));
        }
    }
}