using System.Diagnostics;
using System.Reflection;
using CodeCampServer.UI.Services;

namespace CodeCampServer.Infrastructure.UI.Services
{
	public class AssemblyVersion : IAssemblyVersion
	{
		public string GetAssemblyVersion()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
			return versionInfo.FileVersion;
		}
	}
}