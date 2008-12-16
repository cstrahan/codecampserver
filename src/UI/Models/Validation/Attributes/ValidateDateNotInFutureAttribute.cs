using Castle.Components.Validator;
using CodeCampServer.UI.Models.Validation.Validators;

namespace CodeCampServer.UI.Models.Validation.Attributes
{
    public class ValidateDateNotInFutureAttribute : AbstractValidationAttribute
    {
        public ValidateDateNotInFutureAttribute()
        {
        }

        public ValidateDateNotInFutureAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public override IValidator Build()
        {
            IValidator validator = new DateNotInFutureValidator();

            ConfigureValidatorMessage(validator);

            return validator;
        }
    }
}