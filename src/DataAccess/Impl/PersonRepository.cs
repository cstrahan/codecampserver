using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using NHibernate;
using NHibernate.Expression;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class PersonRepository : RepositoryBase, IPersonRepository
	{
		public PersonRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}

		public void Save(Person person)
		{
			ISession session = getSession();
			ITransaction transaction = session.BeginTransaction();
			session.SaveOrUpdate(person);
			transaction.Commit();
		}
    }
}