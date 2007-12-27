namespace CodeCampServer.Model.Domain
{
    public class Contact
    {
        private string _firstName;
        private string _lastName;
        private string _email;

        public virtual string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public virtual string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}