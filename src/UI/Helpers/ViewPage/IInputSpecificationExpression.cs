namespace CodeCampServer.UI.Helpers.ViewPage
{
	public interface IInputSpecificationExpression	{
		IInputSpecificationExpression Attributes(object attributes);
		IInputSpecificationExpression Using<T>() where T : IInputBuilder;
		IInputSpecificationExpression WithValue(object value);
	}
}