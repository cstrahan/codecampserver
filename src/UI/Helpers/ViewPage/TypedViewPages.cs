using CodeCampServer.UI.Models.Forms;
using CodeCampServer.UI.Models.ViewModels;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class TrackIndex : BaseViewPage<TrackViewModel[]>
	{
	}

	public class TrackEdit : BaseViewPage<TrackForm>
	{
	}

	public class AdminEditView : BaseViewPage<UserForm>
	{

	}

	public class ControllerEditView : BaseViewPage<ConferenceForm> { }

	public class SessionEditView : BaseViewPage<SessionForm>{}
	public class SessionIndexView : BaseViewPage<SessionForm[]>{}
}