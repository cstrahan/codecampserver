using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.DependencyResolution;
using MbUnit.Framework;
using RegressionTests.TestHelpers;
using Tarantino.Core.Commons.Model;

namespace RegressionTests.Web
{
	[TestFixture, Category("WatiN")]
	public abstract class DataDrivenWebTestBase : WebTest
	{
		protected ObjectMother dataCreator = new ObjectMother();
		protected IDisposable disposableDataContext;

		protected override string UserName
		{
			get
			{
				throw new InvalidOperationException(
					"the UserName property must be overriden for classes that inherit from  DataDrivenWebTestBase");
			}
		}

		protected abstract IEnumerable<PersistentObject> CreateDataForTest();

		private void OnFixtureSetup()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();

			disposableDataContext = new DataCreationHelper().PersistEntities(
				CreateDataForTest().ToArray()
				);
		}

		public override void FixtureSetup()
		{
			OnFixtureSetup();
			base.FixtureSetup();
		}

		public override void FixtureTearDown()
		{
			if (disposableDataContext != null)
				disposableDataContext.Dispose();

			base.FixtureTearDown();
		}
	}
}