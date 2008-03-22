using System;
using CodeCampServer.Model.Domain;
using NHibernate;

namespace CodeCampServer.DataAccess.Impl
{	
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

        public Person FindByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("email must be non-empty.");
            
            ISession session = getSession();
            IQuery query = session.CreateQuery("from Person p where lower(p.Contact.Email) like ?");
            query.SetString(0, email.ToLower());
            query.SetMaxResults(1);

            return query.UniqueResult() as Person;
        }

	    public int GetNumberOfUsers()
	    {
	        IQuery query = getSession().CreateQuery("select count(*) from Person p");
	        object val = query.UniqueResult();

	        return Convert.ToInt32(val);
	    }
	}
}