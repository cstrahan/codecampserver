using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class SpeakerUpdater : ModelUpdater<Speaker, SpeakerForm>, ISpeakerUpdater
	{
		private readonly ISpeakerRepository _repository;

		public SpeakerUpdater(ISpeakerRepository repository)
		{
			_repository = repository;
		}

		protected override IRepository<Speaker> Repository
		{
			get { return _repository; }
		}

		protected override Guid GetIdFromMessage(SpeakerForm message)
		{
			return message.Id;
		}

		protected override void UpdateModel(SpeakerForm message, Speaker model)
		{
			model.Bio = message.Bio;
			model.Company = message.Company;
			model.EmailAddress = message.EmailAddress;
			model.FirstName = message.FirstName;
			model.JobTitle = message.JobTitle;
			model.LastName = message.LastName;
			model.WebsiteUrl = message.WebsiteUrl;
			model.Key = message.Key;
		}

		protected override UpdateResult<Speaker, SpeakerForm> PreValidate(SpeakerForm message)
		{
			if (SpeakerKeyAlreadyExists(message))
			{
				return new UpdateResult<Speaker, SpeakerForm>(false)
					.WithMessage(x => x.Key, "This speaker key already exists");
			}
			return base.PreValidate(message);
		}

		private bool SpeakerKeyAlreadyExists(SpeakerForm message)
		{
			Speaker speaker = ((ISpeakerRepository) Repository).GetByKey(message.Key);
			return speaker != null && speaker.Id!=message.Id;
		}
	}
}