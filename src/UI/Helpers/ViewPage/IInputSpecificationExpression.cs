using System.Collections.Generic;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public interface IInputSpecificationExpression	{
		IInputSpecificationExpression Attributes(IDictionary<string, object> attributes);
		IInputSpecificationExpression Using<T>() where T : IInputBuilder;
		IInputSpecificationExpression WithValue(object value);
	}
}