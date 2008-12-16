using Castle.Components.Validator;

namespace CodeCampServer.UI.Models.Validation.Attributes
{
	public class BetterValidateDecimalAttribute : ValidateDecimalAttribute
	{
		private readonly string _label;

		public BetterValidateDecimalAttribute(string label)
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