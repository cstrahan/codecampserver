using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.UI.Mappers
{
	public class SponsorInputToSponsorTypeConverter : ITypeConverter<SponsorInput, Sponsor>
	{
		private readonly IUserGroupRepository _userGroupRepository;

		public SponsorInputToSponsorTypeConverter(IUserGroupRepository userGroupRepository)
		{
			_userGroupRepository = userGroupRepository;
		}

		public Sponsor Convert(SponsorInput source)
		{
			var usergroup = _userGroupRepository.GetById(source.UserGroupId);
			var sponsor = usergroup.GetSponsors().Where(sponsor1 => sponsor1.Id == source.ID).FirstOrDefault();
			if(sponsor==null)
			{
				sponsor=new Sponsor();
			}
			sponsor.Name = source.Name;
			sponsor.Level = source.Level;
			sponsor.Url = source.Url;
			sponsor.BannerUrl = source.BannerUrl;
			
			return sponsor;
		}
	}
}