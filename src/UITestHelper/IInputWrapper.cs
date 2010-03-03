using System.Linq.Expressions;
using System.Reflection;

namespace UITestHelper
{
	public interface IInputWrapper
	{
        void SetInput(IBrowserDriver browserDriver);
		void AssertInputValueMatches(IBrowserDriver browserDriver);
	}
}