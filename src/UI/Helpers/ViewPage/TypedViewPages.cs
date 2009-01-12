using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class TrackIndexView : BaseViewPage<TrackForm[]> {}

	public class TrackEditView : BaseViewPage<TrackForm> {}

	public class AdminEditView : BaseViewPage<UserForm> {}

	public class ConferenceEditView : BaseViewPage<ConferenceForm> {}

	public class SessionEditView : BaseViewPage<SessionForm> {}

	public class SessionIndexView : BaseViewPage<SessionForm[]> {}

	public class SpeakerEditView : BaseViewPage<SpeakerForm> {}
	public class LoginView : BaseViewPage<LoginForm>{}
}