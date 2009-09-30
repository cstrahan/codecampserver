using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class UserGroupSponsorMapper : AutoInputMapper<UserGroup, SponsorInput>, IUserGroupSponsorMapper
	{
	    public UserGroupSponsorMapper(IRepository<UserGroup> repository) : base(repository) {}

	    protected override Guid GetIdFromMessage(SponsorInput message)
	    {
	        return message.ParentID;
	    }

	    protected override void MapToModel(SponsorInput message, UserGroup model)
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
