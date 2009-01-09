using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class TrackIndex : BaseViewPage<TrackForm[]>
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