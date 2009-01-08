using CodeCampServer.DependencyResolution;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.UI.DataLoader
{
	[TestFixture]
	public class ZDataLoader
	{
		[Test, Category("DataLoader"), Ignore("not complete")]
		public void DataLoader()
		{
//			Logger.EnsureInitialized();
			DependencyRegistrar.EnsureDependenciesRegistered();
		}
	}
}