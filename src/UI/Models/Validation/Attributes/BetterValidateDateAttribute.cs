using Castle.Components.Validator;

namespace CodeCampServer.UI.Models.Validation.Attributes
{
	public class BetterValidateDateAttribute : ValidateDateAttribute
	{
		private readonly string _label;

		public BetterValidateDateAttribute(string label)
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