using System.Collections.Generic;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.RegressionTests.TestHelpers
{
	public class ObjectMother
	{
		private readonly IList<User> _users;

		public ObjectMother()
		{
			_users = new List<User>();
		}

		public IEnumerable<PersistentObject> ToEntityArray()
		{
			var entities = new List<PersistentObject>();

			foreach (User user in _users)
			{
				entities.Add(user);
			}
			return entities;
		}
	}
}