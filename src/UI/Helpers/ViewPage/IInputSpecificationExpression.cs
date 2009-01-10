namespace CodeCampServer.UI.Helpers.ViewPage
{
	public interface IInputSpecificationExpression
	{
		IInputSpecificationExpression NoLabel();
		IInputSpecificationExpression DisplayedInline();
		IInputSpecificationExpression Attributes(object attributes);
		IInputSpecificationExpression WithInvalidOption();
		IInputSpecificationExpression NoCleaner();
	}
}