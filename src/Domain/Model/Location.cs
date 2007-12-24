namespace CodeCampServer.Domain.Model
{
    public class Location
    {
        private string _name;
        private string _address1;
        private string _address2;
        private string _city;
        private string _region;
        private string _postalCode;
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

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string Region
        {
            get { return _region; }
            set { _region = value; }
        }

        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }
    }
}