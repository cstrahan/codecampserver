using Castle.Components.Validator;

namespace CodeCampServer.UI.Helpers.Validation.Attributes
{
	public class RequiredDecimalAttribute : ValidateDecimalAttribute
	{
		private readonly string _label;

		public RequiredDecimalAttribute(string label)
		{
			_label = label;
		}

		public string Label
		{
			get { return _label; }
		}

		public override IValidator Build()
		{
			IValidator validator = new BetterDecimalValidator(_label);

			ConfigureValidatorMessage(validator);

			return validator;
		}

		private class BetterDecimalValidator : DecimalValidator
		{
			private readonly string _label;

			public BetterDecimalValidator(string label)
			{
				_label = label;
			}

			protected override string BuildErrorMessage()
			{
				return string.Format("'{0}' is not a valid decimal ", _label);
			}
		}
	}
}