using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class ConferenceUpdater : ModelUpdater<Conference, ConferenceForm>, IConferenceUpdater
	{
		private readonly IConferenceRepository _repository;

		public ConferenceUpdater(IConferenceRepository repository)
		{
			_repository = repository;
		}

		protected override IRepository<Conference> Repository
		{
			get { return _repository; }
		}

		protected override Guid GetIdFromMessage(ConferenceForm message)
		{
			return message.Id;
		}

		protected override void UpdateModel(ConferenceForm message, Conference model)
		{
			model.Address = message.Address;
			model.City = message.City;
			model.Key = message.Key;
			model.Description = message.Description;
			model.EndDate = ToDateTime(message.EndDate);
			model.LocationName = message.LocationName;
			model.Name = message.Name;
			model.PhoneNumber = message.PhoneNumber;
			model.PostalCode = message.PostalCode;
			model.Region = message.Region;
			model.StartDate = ToDateTime(message.StartDate);
		}

		protected override UpdateResult<Conference, ConferenceForm> PreValidate(ConferenceForm message)
		{
			if (ConferenceKeyAlreadyExists(message))
			{
				return new UpdateResult<Conference, ConferenceForm>(false)
					.WithMessage(x => x.Key, "This conference key already exists");
			}

			return base.PreValidate(message);
		}

		private bool ConferenceKeyAlreadyExists(ConferenceForm message)
		{
			Conference conference = ((IKeyedRepository<Conference>) Repository).GetByKey(message.Key);
			return conference != null && conference.Id!=message.Id;
		}
	}
}