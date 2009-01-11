using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IAttendeeMapper : IMapper<Conference, AttendeeForm>
	{
	}
}