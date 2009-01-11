using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class SessionUpdater : ModelUpdater<Session, SessionForm>, ISessionUpdater
	{
		private readonly ISessionRepository _repository;

		public SessionUpdater(ISessionRepository repository)
		{
			_repository = repository;
		}

		protected override IRepository<Session> Repository
		{
			get { return _repository; }
		}

		protected override Guid GetIdFromMessage(SessionForm message)
		{
			return message.Id;
		}

		protected override void UpdateModel(SessionForm message, Session model)
		{
			model.Track = message.Track;
			model.TimeSlot = message.TimeSlot;
			model.Speaker = message.Speaker;
			model.Conference = message.Conference;
			model.RoomNumber = message.RoomNumber;
			model.Title = message.Title;
			model.Abstract = message.Abstract;
			model.Level = message.Level;
			model.MaterialsUrl = message.MaterialsUrl;
			model.Key = message.Key;
		}

		protected override UpdateResult<Session, SessionForm> PreValidate(SessionForm message)
		{
			if (SpeakerKeyAlreadyExists(message))
			{
				return new UpdateResult<Session, SessionForm>(false).WithMessage(x => x.Key, "This session key already exists");
			}
			return base.PreValidate(message);
		}


		private bool SpeakerKeyAlreadyExists(SessionForm message)
		{
			return _repository.GetByKey(message.Key) != null;
		}
	}
}