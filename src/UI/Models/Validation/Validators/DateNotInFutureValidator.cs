using System;
using Castle.Components.Validator;
using CodeCampServer.Core;

namespace CodeCampServer.UI.Models.Validation.Validators
{
    public class DateNotInFutureValidator : DateTimeValidator
    {
        public override bool IsValid(object instance, object fieldValue)
        {
            if (fieldValue == null || fieldValue.ToString() == "") return true;

            if (!base.IsValid(instance, fieldValue))
                return false;

            DateTime date = DateTime.Parse((string) fieldValue);

            return (date < SystemTime.Now());
        }
    }
}