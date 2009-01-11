using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class TrackUpdater : ModelUpdater<Track, TrackForm>, ITrackUpdater
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

		protected override Guid GetIdFromMessage(TrackForm message)
		{
			return message.Id;
		}

		protected override void UpdateModel(TrackForm message, Track model)
		{
			model.Name = message.Name;
			model.Conference = _conferenceRepository.GetById(message.ConferenceId);
		}
	}
}