using System;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
        {
        }

        #region IUserRepository Members

        public User GetByUserName(string username)
        {
            ISession session = GetSession();
            IQuery query = session.CreateQuery("from User u where u.Username = :username");
            query.SetString("username", username);

            var matchingUser = query.UniqueResult<User>();

            return matchingUser;
        }

        public void Save(User user)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            session.SaveOrUpdate(user);
            transaction.Commit();
        }

        public User GetById(Guid id)
        {
            return GetSession().Load<User>(id);
        }

        public User[] GetAll()
        {
            ISession session = GetSession();
            IQuery query = session.CreateQuery("from User");
            return query.List<User>().ToArray();
        }

        public User[] GetLikeLastNameStart(string query)
        {
            return GetSession()
                .CreateCriteria(typeof (User))
                .Add(Restrictions.InsensitiveLike("Name", query, MatchMode.Start))
                .AddOrder(Order.Asc("Name"))
                .List<User>()
                .ToArray();
        }

        #endregion
    }
}