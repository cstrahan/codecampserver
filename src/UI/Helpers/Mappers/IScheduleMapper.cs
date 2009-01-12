using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IScheduleMapper
	{
		ScheduleForm Map(Conference conference);
	}
}