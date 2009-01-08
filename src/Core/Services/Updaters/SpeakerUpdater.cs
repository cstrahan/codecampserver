using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;

namespace CodeCampServer.Core.Services.Updaters
{
	public class SpeakerUpdater : ModelUpdater<Speaker, ISpeakerMessage>, ISpeakerUpdater
	{
		private readonly ISpeakerRepository _repository;

		public SpeakerUpdater(ISpeakerRepository repository)
		{
			_repository = repository;
		}

		public override IRepository<Speaker> Repository
		{
			get { return _repository; }
		}

		protected override Guid GetIdFromMessage(ISpeakerMessage message)
		{
			return message.Id;
		}

		protected override void UpdateModel(ISpeakerMessage message, Speaker model)
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

		protected override UpdateResult<Speaker, ISpeakerMessage> PreValidate(ISpeakerMessage message)
		{
			if (SpeakerKeyAlreadyExists(message))
			{
				return new UpdateResult<Speaker, ISpeakerMessage>(false).WithMessage(x => x.Key, "This speaker key already exists");
			}
			return base.PreValidate(message);
		}

		private bool SpeakerKeyAlreadyExists(ISpeakerMessage message)
		{
			return ((ISpeakerRepository) Repository).GetByKey(message.Key) != null;
		}
	}
}