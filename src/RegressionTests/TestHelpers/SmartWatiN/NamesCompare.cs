using WatiN.Core.Interfaces;

namespace RegressionTests.Web
{
    public class NamesCompare : ICompare
    {
        private readonly string _toMatch;

        public NamesCompare(string toMatch)
        {
            _toMatch = toMatch;
        }

        public bool Compare(string value)
        {
            if (value == null)
                return false;

            foreach (var name in value.Split(' '))
            {
                if (name.Equals(_toMatch))
                    return true;
            }

            return false;
        }
    }
}