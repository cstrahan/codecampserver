using System.IO;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Web;

namespace CodeCampServer.Infrastructure.UI.Services.Impl
{
	public class UploadFileNamer : IUploadFileNamer
	{
		private readonly IWebContext _context;
		private readonly IGuidGenerator _guidGenerator;

		public UploadFileNamer(IWebContext context, IGuidGenerator guidGenerator)
		{
			_context = context;
			_guidGenerator = guidGenerator;
		}

		public string GetUploadFilename()
		{
			var path = Path.Combine(_context.GetPhysicalApplicationPath(), "UploadedFiles");
			var filename = string.Format("{0}.txt", _guidGenerator.CreateGuid());
			var fullFilename = Path.Combine(path, filename);

			return fullFilename;
		}
	}
}