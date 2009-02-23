using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface ISessionMapper : IMapper<Session, SessionForm>
	{
		SessionForm[] Map(Session[] sessions);
	}
}