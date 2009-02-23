using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class SpeakerMapper : AutoFormMapper<Speaker, SpeakerForm>, ISpeakerMapper
	{
		public SpeakerMapper(ISpeakerRepository repository) : base(repository)
		{
		}

		protected override Guid GetIdFromMessage(SpeakerForm form)
		{
			return form.Id;
		}

		protected override void MapToModel(SpeakerForm form, Speaker model)
		{
			model.Bio = form.Bio;
			model.Company = form.Company;
			model.EmailAddress = form.EmailAddress;
			model.FirstName = form.FirstName;
			model.JobTitle = form.JobTitle;
			model.LastName = form.LastName;
			model.WebsiteUrl = form.WebsiteUrl;
			model.Key = form.Key;
		}


		public SpeakerForm[] Map(Speaker[] speakers)
		{
			return Map<Speaker[], SpeakerForm[]>(speakers);
		}
	}
}