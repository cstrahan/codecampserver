using System.Net;
using CodeCampServer.Core.Services;

namespace CodeCampServer.UI.Services
{
	public class HttpFileDownloaderService : IHttpFileDownloaderService
	{
		public string GetStringFromUrl(string url)
		{
			var webClient = new WebClient();
			try
			{
				string fileContents = webClient.DownloadString(url);
				return fileContents;
			}
			catch
			{
				return string.Empty;
			}
		}
	}
}