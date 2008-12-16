namespace RegressionTests.Web
{
    public class Checkable
    {
        private readonly bool _check;

        public Checkable(bool check)
        {
            _check = check;
        }

        public bool Matches(bool value)
        {
            return Is(value);
        }

        public bool Is(bool value)
        {
            bool result = value == _check;
            return result;
        }
    }
}