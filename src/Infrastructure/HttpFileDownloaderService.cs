using System.Net;
using RssRepository.UnitTests.Services;

namespace RssRepository.Services.Impl
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