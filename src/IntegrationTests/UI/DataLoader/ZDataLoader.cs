using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.DataAccess;
using NUnit.Framework;
using StructureMap;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.IntegrationTests.DataLoader
{
	[TestFixture]
	public class ZDataLoader 
	{

		[Test, Category("DataLoader"),Ignore("not complete")]
		public void DataLoader()
		{
//			Logger.EnsureInitialized();
			DependencyRegistrar.EnsureDependenciesRegistered();
			var sessionBuilder = ObjectFactory.GetInstance<ISessionBuilder>();
			var dataDeleter = ObjectFactory.GetInstance<IDatabaseDeleter>();
		}
	}
}