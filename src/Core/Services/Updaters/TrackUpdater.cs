using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;

namespace CodeCampServer.Core.Services.Updaters
{
	public class TrackUpdater : ModelUpdater<Track, ITrackMessage>, ITrackUpdater
	{
		private readonly ITrackRepository _repository;
		private readonly IConferenceRepository _conferenceRepository;

		public TrackUpdater(ITrackRepository repository, IConferenceRepository conferenceRepository)
		{
			_repository = repository;
			_conferenceRepository = conferenceRepository;
		}

		protected override IRepository<Track> Repository
		{
			get { return _repository; }
		}

		protected override Guid GetIdFromMessage(ITrackMessage message)
		{
			return message.Id;
		}

		protected override void UpdateModel(ITrackMessage message, Track model)
		{
			model.Name = message.Name;
			model.Conference = _conferenceRepository.GetById(message.ConferenceId);
		}
	}
}