using CodeCampServer.UI.Helpers.ViewPage;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class NullInputBuilder : BaseInputCreator
	{
		public NullInputBuilder(InputBuilder inputBuilder)
			: base(inputBuilder)
		{
		}

		protected override bool OutputEmpty
		{
			get { return true; }
		}

		protected override string CreateInputElementBase()
		{
			return "";
		}
	}
}