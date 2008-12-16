using WatiN.Core.Interfaces;

namespace RegressionTests.Web
{
    public class HasAttrCompare : ICompare
    {
        public bool Compare(string value)
        {
            if (value == null)
                return true;

            return false;
        }
    }
}