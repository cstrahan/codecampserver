namespace RegressionTests.Web
{
    public class Until
    {
        private readonly string _until;

        public Until(string until)
        {
            _until = until;
        }

        public string s
        {
            get { return _until; }
        }

        public override string ToString()
        {
            return string.Format("[{0}]", _until);
        }
    }
}