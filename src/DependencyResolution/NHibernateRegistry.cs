using System;
using NHibernate;
using NHibernate.Cfg;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public class NHibernateRegistry : Registry
	{
		public NHibernateRegistry()
		{
			ForRequestedType<IUnitOfWork>()
				.TheDefaultIsConcreteType<UnitOfWork>();

			ForRequestedType<Tarantino.RulesEngine.IUnitOfWork>().TheDefault.Is.ConstructedBy(
				ctx => ctx.GetInstance<IUnitOfWork>());
		}
	}
}