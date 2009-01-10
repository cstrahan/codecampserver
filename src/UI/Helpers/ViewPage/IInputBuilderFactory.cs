using CodeCampServer.UI.Helpers.ViewPage;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public interface IInputBuilderFactory
	{
		IInputBuilder FindInputBuilderFor(IInputSpecification inputSpecification);
	}
}