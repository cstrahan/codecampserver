using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Castle.Components.Validator;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public abstract class BaseInputBuilder : IInputBuilder
	{
		private IInputSpecification _inputSpecification;

		protected virtual IInputSpecification InputSpecification
		{
			get { return _inputSpecification; }
		}

		protected virtual bool UseSpanAsLabel
		{
			get { return _inputSpecification.UseSpanAsLabel; }
		}

		public virtual string Build(IInputSpecification specification)
		{
			_inputSpecification = specification;

			var output = new StringBuilder("<span class=\"inputElement\">");

			AppendLabel(output);

			AppendInputElement(output);

			AppendValidationError(output);

			AppendExampleText(output);

			output.Append("</span>");

			AppendCleaner(output);

			return output.ToString();
		}

		public abstract bool IsSatisfiedBy(IInputSpecification specification);

		protected abstract string CreateInputElementBase();

		protected virtual string GetLabelFor()
		{
			return InputSpecification.InputId;
		}

		protected virtual string ProcessId(string element)
		{
			string oldid = string.Format("id=\"{0}\"", InputSpecification.InputName);
			string newid = string.Format("id=\"{0}\"", InputSpecification.InputId);
			return element.Replace(oldid, newid);
		}

		protected virtual object GetValue()
		{
			object inputValue = InputSpecification.InputValue;
			if (inputValue != null)
			{
				return inputValue;
			}

			return ExpressionHelper.Evaluate(InputSpecification.Expression, InputSpecification.Helper.ViewData.Model);
		}

		protected virtual void AppendCleaner(StringBuilder output)
		{
			output.Append("<div class='cleaner'></div>");
		}

		private void AppendValidationError(StringBuilder output)
		{
			bool isInvalid = IsInvalid();
			string invalidAltText = GetErrorLabel();
			if (InputSpecification.InputIndex != null)
			{
				invalidAltText += " " + InputSpecification.InputIndex;
			}

			if (isInvalid)
				output.AppendFormat(
					@"<img src=""/images/icons/exclamation.png"" alt=""Validation error on '{0}'"" class=""errorIndicator"" />",
					invalidAltText);
		}

		protected virtual void AppendLabel(StringBuilder output)
		{
			string labelClass = "";

			if (UseSpanAsLabel)
				output.AppendFormat(@"<span class=""label"">");
			else
			{
				string style = "";
				output.AppendFormat(@"<label for=""{0}"" class=""{1}"" style=""{2}"">", GetLabelFor(), labelClass, style);
			}

			if (IsRequired())
			{
				output.Append(@"<span class=""requiredFieldIndicator"">*</span>");
			}

			output.AppendFormat(@"{0}:", GetLabel());

			if (UseSpanAsLabel)
				output.Append("</span>");
			else
				output.Append("</label>");
		}

		private bool IsInvalid()
		{
			return InputSpecification.Helper.ViewData.ModelState.ContainsKey(InputSpecification.InputName) &&
			       InputSpecification.Helper.ViewData.ModelState[InputSpecification.InputName].Errors.Count > 0;
		}

		private bool IsRequired()
		{
			return InputSpecification.PropertyInfo.HasCustomAttribute<ValidateNonEmptyAttribute>() ||
			       InputSpecification.PropertyInfo.HasCustomAttribute<ShowAsRequiredAttribute>();
		}

		private void AppendInputElement(StringBuilder output)
		{
			string element = CreateInputElementBase();
			Debug.WriteLine(element);
			string elementAfterIdProcessing = ProcessId(element);

			output.Append(elementAfterIdProcessing);
		}

		private void AppendExampleText(StringBuilder output)
		{
			string exampleText = GetExampleText();
			if (!string.IsNullOrEmpty(exampleText))
				output.AppendFormat("<span class=\"example\">ex.: {0}</span>", exampleText);
		}

		private string GetLabel()
		{
			if (InputSpecification.PropertyInfo.HasCustomAttribute<LabelAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<LabelAttribute>().Value;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateNonEmptyAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateNonEmptyAttribute>().Label;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateDateAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateDateAttribute>().Label;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateDateTimeAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateDateTimeAttribute>().Label;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateEmailAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateEmailAttribute>().Label;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateDecimalAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateDecimalAttribute>().Label;
			}

			return InputSpecification.PropertyInfo.Name.ToSeparatedWords();
		}

		private string GetErrorLabel()
		{
			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateNonEmptyAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateNonEmptyAttribute>().Label;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateDateAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateDateAttribute>().Label;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateDateTimeAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateDateTimeAttribute>().Label;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateEmailAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateEmailAttribute>().Label;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<BetterValidateDecimalAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<BetterValidateDecimalAttribute>().Label;
			}

			if (InputSpecification.PropertyInfo.HasCustomAttribute<LabelAttribute>())
			{
				return InputSpecification.PropertyInfo.GetCustomAttribute<LabelAttribute>().Value;
			}

			return InputSpecification.PropertyInfo.Name.ToSeparatedWords();
		}

		private string GetExampleText()
		{
			if (InputSpecification.PropertyInfo.HasCustomAttribute<ValidateDecimalAttribute>())
				return "1234.56, 12,234.00, 12345";

			return null;
		}

		protected static void MergeAttribute(IDictionary<string, object> attributes, string key, object value)
		{
			if (!attributes.ContainsKey(key))
			{
				attributes.Add(key, value);
			}
		}
	}
}