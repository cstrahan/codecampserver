namespace CodeCampServer.UI.ViewPage
{
	public interface IInputBuilderFactory
	{
		BaseInputCreator CreateInputCreator(InputBuilder inputBuilder);
	}
}