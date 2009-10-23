using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IConferenceMapper : IMapper<Conference, ConferenceInput> {
		ConferenceInput[] Map(Conference[] conferences);
	}
}