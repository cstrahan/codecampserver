using Castle.Components.Validator;

namespace CodeCampServer.UI.Helpers.Validation.Attributes
{
	public class RequiredDateAttribute : ValidateDateAttribute
	{
		private readonly string _label;

		public RequiredDateAttribute(string label)
		{
			_label = label;
		}

		public string Label
		{
			get { return _label; }
		}

		public override IValidator Build()
		{
			IValidator validator = new BetterDateValidator(_label);

			ConfigureValidatorMessage(validator);

			return validator;
		}

		private class BetterDateValidator : DateValidator
		{
			private readonly string _label;

			public BetterDateValidator(string label)
			{
				_label = label;
			}

			protected override string BuildErrorMessage()
			{
				return string.Format("'{0}' is not a valid date format", _label);
			}
		}
	}
}