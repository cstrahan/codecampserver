using System;
using System.Collections.Generic;
using System.Reflection;
using CodeCampServer.Core.Common;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.NHibernate;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using CodeCampServer.UnitTests;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public abstract class IntegrationTestBase : TestBase
	{
		private readonly IDictionary<Type, Object> injectedInstances = new Dictionary<Type, Object>();
		private IUnitOfWork _unitOfWork;

		[TestFixtureSetUp]
		public virtual void FixtureSetup()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			SessionBuilder.GetDefault = () => null;
		}

		[SetUp]
		public virtual void Setup()
		{
			injectedInstances.Clear();
			ObjectFactory.Inject(typeof (IUnitOfWork), UnitOfWork);
		}

		protected virtual ISession GetSession()
		{
			return UnitOfWork.GetSession();
		}

		protected virtual void CommitChanges()
		{
			new SessionBuilder().GetSession().Flush();
		}


		protected IUnitOfWork UnitOfWork
		{
			get
			{
				if(_unitOfWork == null)
				{
					_unitOfWork = new UnitOfWork(new SessionBuilder());
				}
				return _unitOfWork;
			}
		}

		protected TPluginType GetInstance<TPluginType>()
		{
			ExplicitArgsExpression expression = ObjectFactory.With(_unitOfWork);
			injectedInstances.Keys.ForEach(type => { expression = expression.With(type, injectedInstances[type]); });
			return expression.GetInstance<TPluginType>();
		}

		/// <summary>
		///   Checks for equality and that all properties' values are equal.
		/// </summary>
		/// <param name="obj1"></param>
		/// <param name="obj2"></param>
		protected static void AssertObjectsMatch(object obj1, object obj2)
		{
			Assert.AreNotSame(obj1, obj2);
			Assert.AreEqual(obj1, obj2);

			PropertyInfo[] infos = obj1.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var info in infos)
			{
				object value1 = info.GetValue(obj1, null);
				object value2 = info.GetValue(obj2, null);
				Assert.AreEqual(value1, value2, string.Format("Property {0} doesn't match", info.Name));
			}
		}
	}
}