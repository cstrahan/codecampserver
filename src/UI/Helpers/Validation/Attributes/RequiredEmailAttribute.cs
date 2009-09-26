using Castle.Components.Validator;

namespace CodeCampServer.UI.Helpers.Validation.Attributes
{
	public class RequiredEmailAttribute : ValidateEmailAttribute
	{
		private readonly string _label;

		public RequiredEmailAttribute(string label)
		{
			_label = label;
		}

		public string Label
		{
			get { return _label; }
		}

		public override IValidator Build()
		{
			IValidator validator = new BetterEmailValidator(_label);

			ConfigureValidatorMessage(validator);

			return validator;
		}

		private class BetterEmailValidator : EmailValidator
		{
			private readonly string _label;

			public BetterEmailValidator(string label)
			{
				_label = label;
			}

			protected override string BuildErrorMessage()
			{
				return string.Format("'{0}' is not a valid email address ", _label);
			}
		}
	}
}