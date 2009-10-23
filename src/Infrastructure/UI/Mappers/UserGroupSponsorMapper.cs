using System;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.UI.Mappers
{
	public class UserGroupSponsorMapper : AutoInputMapper<UserGroup, SponsorInput>, IUserGroupSponsorMapper
	{
		public UserGroupSponsorMapper(IRepository<UserGroup> repository) : base(repository) {}

		protected override Guid GetIdFromMessage(SponsorInput message)
		{
			return message.UserGroupId;
		}

		protected override void MapToModel(SponsorInput message, UserGroup model)
		{
			Sponsor[] sponsors = model.GetSponsors();

			Sponsor sponsorToUpdate = sponsors.Where(sponsor => sponsor.Id == message.ID).FirstOrDefault();

			if (sponsorToUpdate == null)
			{
				model.Add(new Sponsor
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

		public SponsorInput[] Map(Sponsor[] sponsors)
		{
			return Mapper.Map<Sponsor[],SponsorInput[]>(sponsors);
		}

		public SponsorInput Map(Sponsor sponsor)
		{
			return Mapper.Map<Sponsor, SponsorInput>(sponsor);
		}
	}
}