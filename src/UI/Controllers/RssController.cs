using System.Text;
using System.Web.Mvc;
using RssRepository.UnitTests.Services;

namespace CodeCampServer.UI.Controllers
{
    public class RssController : Controller
    {
        private readonly IHttpFileDownloaderService _fileDownloaderService;

        public RssController(IHttpFileDownloaderService fileDownloaderService)
        {
            _fileDownloaderService = fileDownloaderService;
        }

        public ActionResult Index(string url)
        {
            string xml = _fileDownloaderService.GetStringFromUrl(url);
            return File(new ASCIIEncoding().GetBytes(xml), "text/xml");
        }
    }
}