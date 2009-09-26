using Castle.Components.Validator;

namespace CodeCampServer.UI.Helpers.Validation.Attributes
{
	public class RequiredDateTimeAttribute : ValidateDateTimeAttribute
	{
		private readonly string _label;

		public RequiredDateTimeAttribute(string label)
		{
			_label = label;
		}

		public string Label
		{
			get { return _label; }
		}

		public override IValidator Build()
		{
			IValidator validator = new BetterDateTimeValidator(_label);

			ConfigureValidatorMessage(validator);

			return validator;
		}

		private class BetterDateTimeValidator : DateTimeValidator
		{
			private readonly string _label;

			public BetterDateTimeValidator(string label)
			{
				_label = label;
			}

			protected override string BuildErrorMessage()
			{
				return string.Format("'{0}' is not a valid date time format", _label);
			}
		}
	}
}