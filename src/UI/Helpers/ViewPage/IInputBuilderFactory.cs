using CodeCampServer.UI.Helpers.ViewPage;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public interface IInputBuilderFactory
	{
		BaseInputCreator CreateInputCreator(InputBuilder inputBuilder);
	}
}