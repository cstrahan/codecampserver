using System.Diagnostics;
using System.Reflection;

namespace CodeCampServer.UI.Services
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