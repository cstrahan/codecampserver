using Castle.Components.Validator;

namespace CodeCampServer.UI.Models.Validation.Attributes
{
	public class BetterValidateDateTimeAttribute : ValidateDateTimeAttribute
	{
		private readonly string _label;

		public BetterValidateDateTimeAttribute(string label)
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