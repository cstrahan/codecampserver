using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.DependencyResolution;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture]
	public static class TestHelper
	{
		public static User CurrentUser;

		static TestHelper()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
		}

		private static bool _databaseRecreated;

		private static void RecreateDatabase()
		{
			var exporter = new SchemaExport(GetConfiguration());
			exporter.Execute(false, true, false);
		}

		public static ISessionFactory GetSessionFactory()
		{
			return ObjectFactory.GetInstance<ISessionFactory>();
		}

		public static Configuration GetConfiguration()
		{
			return ObjectFactory.GetInstance<Configuration>();
		}

		public static void DeleteAllObjects()
		{
			RecreateDatabase();
		}

		public static void EnsureDatabaseRecreated()
		{
			if (!_databaseRecreated)
			{
				RecreateDatabase();
				_databaseRecreated = true;
			}
		}

		public static void ResetCurrentUser()
		{
			var userSession = MockRepository.GenerateStub<IUserSession>();
			CurrentUser = new User();
			userSession.Stub(us => us.GetCurrentUser()).Return(CurrentUser);
			ObjectFactory.Inject(userSession);
		}
	}
}