using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface ITrackMapper : IMapper<Track, TrackForm>
	{
		TrackForm[] Map(Track[] tracks);
	}
}