using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class UserGroupSponsorMapper : AutoFormMapper<UserGroup, SponsorForm>, IUserGroupSponsorMapper
	{
	    public UserGroupSponsorMapper(IRepository<UserGroup> repository) : base(repository) {}

	    protected override Guid GetIdFromMessage(SponsorForm message)
	    {
	        return message.ParentID;
	    }

	    protected override void MapToModel(SponsorForm message, UserGroup model)
	    {
            var sponsors = model.GetSponsors();
            
            var sponsorToUpdate = sponsors.Where(sponsor => sponsor.Id==message.ID ).FirstOrDefault();
		    
            if(sponsorToUpdate==null)
            {
                model.Add(new Sponsor()
                              {
                                  Level = message.Level, 
                                  Name = message.Name, 
                                  Url = message.Url,
                                  BannerUrl = message.BannerUrl
                              });        
            }
            else
            {
                sponsorToUpdate.Name = message.Name;
                sponsorToUpdate.Level = message.Level;
                sponsorToUpdate.Url = message.Url;
                sponsorToUpdate.BannerUrl = message.BannerUrl;
            }	                 
	    }
	}
}
