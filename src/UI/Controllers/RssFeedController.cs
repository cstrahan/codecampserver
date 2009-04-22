using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Components.Validator.Attributes;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using CodeCampServer.Core.Services.Impl;
using MvcContrib;
using CodeCampServer.UI;
using CodeCampServer.Core.Services;


namespace CodeCampServer.UI.Controllers
{
    public class RssFeedController : SaveController<UserGroup, RssFeedForm>
    {
        private readonly IUserGroupRepository _repository;
        private readonly IUserGroupRssFeedMapper _mapper;
        private readonly ISecurityContext _securityContext;

        public RssFeedController(IUserGroupRepository repository, IUserGroupRssFeedMapper mapper, ISecurityContext securityContext)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _securityContext = securityContext;
        }


        public ActionResult Index(UserGroup usergroup)
        {
            var entities = usergroup.GetRssFeeds();

            var entityListDto = (RssFeedForm[]) AutoMapper.Mapper.Map(entities, typeof(RssFeed[]), typeof(RssFeedForm[]));
            
            return View(entityListDto);
        }

        public ActionResult New(UserGroup userGroup)
        {
            return View("edit",new RssFeedForm());  
        }

        [ValidateModel(typeof(RssFeedForm))]
        public ActionResult Save(UserGroup userGroup, RssFeedForm RssFeedForm)
        {
            RssFeedForm.ParentID = userGroup.Id;
            return ProcessSave(RssFeedForm, entity => RedirectToAction<RssFeedController>(s => s.Index(null)));
        }

        public ActionResult Delete(UserGroup userGroup, RssFeed RssFeed)
        {
            userGroup.Remove(RssFeed);
            _repository.Save(userGroup);
            return RedirectToAction<RssFeedController>(c => c.Index(null));
        }


        public ActionResult Edit(UserGroup userGroup, Guid RssFeedID)
        {
            var RssFeed = userGroup.GetRssFeeds().Where(RssFeed1 => RssFeed1.Id == RssFeedID).FirstOrDefault();

            return View(AutoMapper.Mapper.Map<RssFeed, RssFeedForm>(RssFeed));
        }

        public ActionResult List(UserGroup userGroup)
        {
            var entities = userGroup.GetRssFeeds();

            var entityListDto = (RssFeedForm[]) AutoMapper.Mapper.Map(entities, typeof(RssFeed[]), typeof(RssFeedForm[]));

            return View("HomePageWidget", entityListDto);
        }
    }
}



