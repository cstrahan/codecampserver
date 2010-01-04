using CodeCampServer.Core.Domain;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl;
using CodeCampServer.UI.InputBuilders;
using Microsoft.Win32;
using MvcContrib.UI.InputBuilder;
using StructureMap;
using StructureMap.Configuration.DSL;
using UITestHelper;
using Registry=StructureMap.Configuration.DSL.Registry;

namespace CodeCampServerUiTests
{
	public class UITestRegistry : Registry
	{
		public UITestRegistry()
		{
			ObjectFactory.Configure(x =>
			                        	{
											x.ForRequestedType(typeof(IRepository<>)).TheDefaultIsConcreteType(typeof(RepositoryBase<>));
			                        	});
			new InitiailizeDefaultFactories().Configure();

			ObjectFactory.Configure(x =>
			                        	{
			                        		x.ForRequestedType<IFormInputManipulator<InputBuilderPropertyConvention, WatinDriver>>().
			                        			TheDefaultIsConcreteType<InputBuilderPropertyConventionManipulator>();
			                        		x.ForRequestedType<IFormInputManipulator<MultiLinePropertyConvention, WatinDriver>>().
			                        			TheDefaultIsConcreteType<MultiLinePropertyConventionManipulator>();
			                        	});

			InputBuilder.SetPropertyConvention(() => new InputBuilderPropertyFactory());
		}
	}
}