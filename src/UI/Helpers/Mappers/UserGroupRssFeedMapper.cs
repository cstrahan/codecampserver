using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class UserGroupRssFeedMapper : AutoFormMapper<UserGroup, RssFeedForm>, IUserGroupRssFeedMapper
	{
	    public UserGroupRssFeedMapper(IRepository<UserGroup> repository) : base(repository) {}

	    protected override Guid GetIdFromMessage(RssFeedForm message)
	    {
	        return message.ParentID;
	    }

        protected override void MapToModel(RssFeedForm message, UserGroup model)
	    {
            throw new NotImplementedException();
                 
	    }
	}
}
