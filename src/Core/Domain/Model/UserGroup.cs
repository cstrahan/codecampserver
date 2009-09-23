using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCampServer.Core.Domain.Model
{
	public class UserGroup : KeyedObject
	{
		private readonly IList<User> _users = new List<User>();
		private readonly IList<Sponsor> _sponsors = new List<Sponsor>();
		public virtual string Name { get; set; }
		public virtual string HomepageHTML { get; set; }
		public virtual string City { get; set; }
		public virtual string Region { get; set; }
		public virtual string Country { get; set; }
		public virtual string GoogleAnalysticsCode { get; set; }
		public virtual string DomainName { get; set; }

		public virtual void Add(Sponsor child)
		{
			_sponsors.Add(child);
		}

		public virtual void Remove(Sponsor child)
		{
			_sponsors.Remove(child);
		}

	
		public virtual Sponsor[] GetSponsors()
		{
			return _sponsors.ToArray();
		}


		public virtual void Add(User child)
		{
			_users.Add(child);
		}

		public virtual void Remove(User child)
		{
			_users.Remove(child);
		}


		public virtual User[] GetUsers()
		{
			return _users.ToArray();
		}

		public virtual bool IsDefault()
		{
			return Key.Equals("localhost", StringComparison.InvariantCultureIgnoreCase);
		}
	}
}