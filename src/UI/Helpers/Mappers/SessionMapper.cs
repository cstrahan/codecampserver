using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class SessionMapper : FormMapper<Session, SessionForm>, ISessionMapper
	{
		public SessionMapper(ISessionRepository repository) : base(repository)
		{
		}

		protected override Guid GetIdFromMessage(SessionForm form)
		{
			return form.Id;
		}

		protected override void MapToModel(SessionForm form, Session model)
		{
			model.Track = form.Track;
			model.TimeSlot = form.TimeSlot;
			model.Speaker = form.Speaker;
			model.Conference = form.Conference;
			model.RoomNumber = form.RoomNumber;
			model.Title = form.Title;
			model.Abstract = form.Abstract;
			model.Level = form.Level;
			model.MaterialsUrl = form.MaterialsUrl;
			model.Key = form.Key;
		}

		public SessionForm[] Map(Session[] sessions)
		{
			return Map<Session[], SessionForm[]>(sessions);
		}
	}
}