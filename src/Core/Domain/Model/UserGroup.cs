using System.Collections.Generic;
using System.Linq;

namespace CodeCampServer.Core.Domain.Model
{
    public class UserGroup : KeyedObject
    {
        private readonly IList<User> _users = new List<User>();
        public virtual string Name { get; set; }
        public virtual string HomepageHTML { get; set; }
        public virtual string City { get; set; }
        public virtual string Region { get; set; }
        public virtual string Country { get; set; }
        public virtual string GoogleAnalysticsCode { get; set; }

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
    }
}