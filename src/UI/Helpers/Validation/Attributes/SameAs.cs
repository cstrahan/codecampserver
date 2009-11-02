using System;

namespace CodeCampServer.UI.Helpers.Validation.Attributes
{
	public class SameAs:System.ComponentModel.DataAnnotations.ValidationAttribute
	{
		private readonly string _referenceProperty;
		private readonly string _propertyToCompare;

		public SameAs(string referenceProperty,string propertyToCompare)
		{
			_referenceProperty = referenceProperty;
			_propertyToCompare = propertyToCompare;
		}

		public override bool IsValid(object value)
		{
			var valueToCompare = value.GetType().GetProperty(_propertyToCompare).GetValue(value,new object[0]);
			var refValue = value.GetType().GetProperty(_referenceProperty).GetValue(value, new object[0]);
			return valueToCompare.Equals(refValue);
		}
	}
}