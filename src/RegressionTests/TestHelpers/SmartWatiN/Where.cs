using System.Linq;
using RegressionTests.TestHelpers.SmartWatiN;
using WatiN.Core;

namespace RegressionTests.Web
{
    public class Where
    {
        private readonly Element[] _elements;
        private readonly string _locateIdentifier;

        public Where(Element[] elements, string locateIdentifier)
        {
            _elements = elements;
            _locateIdentifier = locateIdentifier;
        }

        public Element[] IsEqualTo(string value)
        {
            return _elements.Where(x => ElementExtensions.Locate((Element) x, _locateIdentifier).Text == value).ToArray();
        }

        public Element[] IsNotEqualTo(string value)
        {
            return _elements.Where(x => x.Locate(_locateIdentifier).Text != value).ToArray();
        }
    }
}