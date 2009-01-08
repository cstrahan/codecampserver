using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters.Impl;

namespace CodeCampServer.Core.Services.Updaters.Impl
{
	public class SessionUpdater : ModelUpdater<Session, ISessionMessage>,
	                              ISessionUpdater
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

		protected override Guid GetIdFromMessage(ISessionMessage message)
		{
			return message.Id;
		}

		protected override void UpdateModel(ISessionMessage message, Session model)
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

		protected override UpdateResult<Session, ISessionMessage> PreValidate(ISessionMessage message)
		{
			if (SpeakerKeyAlreadyExists(message))
			{
				return new UpdateResult<Session, ISessionMessage>(false).WithMessage(x => x.Key, "This session key already exists");
			}
			return base.PreValidate(message);
		}

		
		private bool SpeakerKeyAlreadyExists(ISessionMessage message)
		{
			return _repository.GetByKey(message.Key) != null;
		}
	}
}