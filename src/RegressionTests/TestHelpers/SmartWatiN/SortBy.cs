using System;
using System.Linq;
using CodeCampServer.RegressionTests.TestHelpers.SmartWatiN;
using WatiN.Core;

namespace CodeCampServer.RegressionTests.TestHelpers.SmartWatiN
{
	public class SortBy
	{
		private readonly Element[] _elements;
		private readonly string _locateIdentifier;
		private readonly Func<string, IComparable> _comparer;

		public SortBy(Element[] elements, string locateIdentifier, Func<string, IComparable> comparer)
		{
			_elements = elements;
			_locateIdentifier = locateIdentifier;
			_comparer = comparer;
		}

		public SortBy(Element[] elements, string locateIdentifier) : this(elements, locateIdentifier, x => x)
		{
		}

		public Element[] Ascending()
		{
			return _elements.OrderBy(x => _comparer.Invoke(ElementExtensions.Locate((Element) x, _locateIdentifier).Text)).ToArray();
		}

		public Element[] Descending()
		{
			return _elements.OrderByDescending(x => _comparer.Invoke(x.Locate(_locateIdentifier).Text)).ToArray();
		}
	}
}