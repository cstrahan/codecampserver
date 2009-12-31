namespace CodeCampServer.UI.Helpers
{
	public interface IPreviousViewPageUrlManager
	{
		string GetPreviousViewPageUrl();
		bool HasPreviousViewPageUrl();
		string GetTargetUrlWithPreviousPage(string targetUrl);
	}
}