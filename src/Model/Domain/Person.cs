using System.Collections.Generic;
using System;
namespace CodeCampServer.Model.Domain
{
    public class Person : EntityBase
    {
        private IList<Role> _roles = new List<Role>();
        public Person()
        {
        }

        internal virtual IList<Role> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        public virtual void AddRole(Role role)
        {
            if (role.Person != this)
                role.Person = this;
            Roles.Add(role);
        }

        public virtual int RoleCount
        {
            get { return Roles.Count; }
        }

        public virtual bool IsInRole(Type roleType)
        {
            foreach (Role role in Roles)
            {
                if (role.GetType() == roleType)
                    return true;
            }
            return false;
        }

        public virtual Role GetRole(Type roleType)
        {
            foreach (Role role in Roles)
                if (role.GetType() == roleType)
                    return role;
            return null;
        }

        private Contact _contact = new Contact();
        private string _website;
        private string _comment;
        private Conference _conference;
        private string _password;
        private string _passwordSalt;

        public Person(string firstName, string lastName, string website,
                        string comment, Conference conference, string email, string password, string passwordSalt)
        {
            _contact.FirstName = firstName;
            _contact.LastName = lastName;
            _contact.Email = email;
            _website = website;
            _comment = comment;
            _conference = conference;
            _password = password;
            _passwordSalt = passwordSalt;
        }

    	public Person(string firstName, string lastName)
    	{
    		_contact.FirstName = firstName;
    		_contact.LastName = lastName;
    	}

    	public virtual Contact Contact
        {
            get { return _contact; }
        }

        public virtual string Website
        {
            get { return _website; }
            set { _website = value; }
        }

        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public virtual string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public virtual string PasswordSalt
        {
            get { return _passwordSalt; }
            set { _passwordSalt = value; }
        }

        public virtual Conference Conference
        {
            get { return _conference; }
            set { _conference = value; }
        }

    	public virtual string GetName()
    	{
			Contact contact = Contact;
			return string.Format("{0} {1}", contact.FirstName, contact.LastName);
    	}
    }
}
