namespace CodeCampServer.UI.Services
{
	public interface IReturnUrlManager
	{
		string GetReturnUrl();
		bool HasReturnUrl();
		string GetTargetUrlWithReturnUrl(string targetUrl);
		string GetCurrentUrl();
	}
}