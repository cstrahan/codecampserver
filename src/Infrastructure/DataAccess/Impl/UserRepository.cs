using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class UserRepository : KeyedRepository<User>, IUserRepository
	{
		public UserRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}

		public User GetByUserName(string username)
		{
			ISession session = GetSession();
			IQuery query = session.CreateQuery("from User u where u.Username = :username");
			query.SetString("username", username);

			var matchingUser = query.UniqueResult<User>();

			return matchingUser;
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

		protected override string GetEntityNaturalKeyName()
		{
			return "Username";
		}
	}
}