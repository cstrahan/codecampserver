namespace Cuc.Jcms.UI.ViewPage.InputBuilders
{
	public class StateDropDownInputBuilder : BaseInputCreator
	{
		public StateDropDownInputBuilder(InputBuilder inputBuilder)
			: base(inputBuilder)
		{
		}

		protected override string CreateInputElementBase()
		{
			return InputBuilder.Helper.StateDropDown(GetCompleteInputName(), InputBuilder.Attributes);
		}
	}
}