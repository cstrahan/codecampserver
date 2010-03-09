using System.Linq;
using CodeCampServer.Core.Domain.Bases;
using NHibernate.Criterion;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public User GetByUserName(string username)
		{
			var session = GetSession();
			var query = session.CreateQuery("from User u where u.Username = :username");
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


		//protected override string GetEntityNaturalKeyName()
		//{
		//    return "Username";
		//}
	}
}