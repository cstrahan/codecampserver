using Castle.Components.Validator;

namespace CodeCampServer.UI.Models.Validation.Attributes
{
	public class BetterValidateIntegerAttribute : ValidateIntegerAttribute
	{
		private readonly string _label;

		public BetterValidateIntegerAttribute(string label)
		{
			_label = label;
		}

		public string Label
		{
			get { return _label; }
		}

		public override IValidator Build()
		{
			IValidator validator = new BetterIntegerValidator(_label);

			ConfigureValidatorMessage(validator);

			return validator;
		}

		private class BetterIntegerValidator : IntegerValidator
		{
			private readonly string _label;

			public BetterIntegerValidator(string label)
			{
				_label = label;
			}

			protected override string BuildErrorMessage()
			{
				return string.Format("'{0}' is not a valid integer ", _label);
			}
		}
	}
}