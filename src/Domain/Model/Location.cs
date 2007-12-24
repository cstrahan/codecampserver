namespace CodeCampServer.Domain.Model
{
    public class Location
    {
        private string _name;
        private string _address1;
        private string _address2;
        private string _phoneNumber;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }
    }
}