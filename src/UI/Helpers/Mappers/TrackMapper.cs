using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class TrackMapper : FormMapper<Track, TrackForm>, ITrackMapper
	{
		private readonly IConferenceRepository _conferenceRepository;

		public TrackMapper(ITrackRepository repository, IConferenceRepository conferenceRepository) : base(repository)
		{
			_conferenceRepository = conferenceRepository;
		}

		protected override Guid GetIdFromMessage(TrackForm form)
		{
			return form.Id;
		}

		protected override void MapToModel(TrackForm form, Track model)
		{
			model.Name = form.Name;
			model.Conference = _conferenceRepository.GetById(form.ConferenceId);
		}

		public TrackForm[] Map(Track[] tracks)
		{
			return Map<Track[], TrackForm[]>(tracks);
		}
	}
}