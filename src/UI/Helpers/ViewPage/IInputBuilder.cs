namespace CodeCampServer.UI.ViewPage
{
	public interface IInputBuilder
	{
		InputBuilder WithInputName(string name);
		InputBuilder WithNoLabel();
		InputBuilder FromValue(object value);
		InputBuilder DisplayedInline();
		InputBuilder WithAttributes(object attributes);
		InputBuilder WithId(string id);
	}
}