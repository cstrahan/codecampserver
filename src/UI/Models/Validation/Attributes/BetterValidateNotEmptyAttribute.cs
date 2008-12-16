using Castle.Components.Validator;

namespace CodeCampServer.UI.Models.Validation.Attributes
{
	public class BetterValidateNonEmptyAttribute : ValidateNonEmptyAttribute
	{
		private readonly string _label;

		public BetterValidateNonEmptyAttribute(string label)
		{
			_label = label;
		}

		public string Label
		{
			get { return _label; }
		}

		public override IValidator Build()
		{
			IValidator validator = new BetterValidateNonEmptyValidator(_label);

			ConfigureValidatorMessage(validator);

			return validator;
		}

		private class BetterValidateNonEmptyValidator : NonEmptyValidator
		{
			private readonly string _label;

			public BetterValidateNonEmptyValidator(string label)
			{
				_label = label;
			}

			protected override string BuildErrorMessage()
			{
				return string.Format("'{0}' is a required field", _label);
			}
		}
	}
}