using System.Text;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Forms.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;
using CodeCampServer.Core;
using CodeCampServer.UI.Models.AutoMap;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public abstract class BaseInputCreator
	{
		private readonly InputBuilder _inputBuilder;

		protected BaseInputCreator(InputBuilder inputBuilder)
		{
			_inputBuilder = inputBuilder;
		}

		protected virtual InputBuilder InputBuilder
		{
			get { return _inputBuilder; }
		}

		protected virtual bool UseSpanAsLabel
		{
			get { return _inputBuilder.UseSpanAsLabel; }
		}

		protected virtual bool OutputEmpty
		{
			get { return false; }
		}

		public string CreateInputElement()
		{
			if (OutputEmpty)
				return string.Empty;

			var propertyName = GetCompleteInputName();

			bool isRequired = IsRequired();

			bool isInvalid = false;

			if (InputBuilder.Helper.ViewData.ModelState.ContainsKey(propertyName) &&
			    InputBuilder.Helper.ViewData.ModelState[propertyName].Errors.Count > 0)
				isInvalid = true;

			var labelClass = InputBuilder.Inline ? "nestedLabel" : "";

			var output = new StringBuilder();

			AppendLabel(output, GetCompleteInputId(), labelClass, isRequired);

			var elementAfterIdProcessing = ProcessId(CreateInputElementBase());

			output.Append(elementAfterIdProcessing);

			AppendValidationError(output, isInvalid);

			AppendCleaner(output);

			var exampleText = GetExampleText();
			if (!string.IsNullOrEmpty(exampleText))
				output.AppendFormat("<span class=\"example\">ex.: {0}</span>", exampleText);

			return output.ToString();
		}

		private bool IsRequired()
		{
			return InputBuilder.PropertyInfo.HasCustomAttribute<ValidateNonEmptyAttribute>() || InputBuilder.PropertyInfo.HasCustomAttribute<ShowAsRequiredAttribute>();
		}

		protected virtual string ProcessId(string element)
		{
			var oldid = string.Format("id=\"{0}\"", GetCompleteInputName());
			var newid = string.Format("id=\"{0}\"", GetCompleteInputId());
			return element.Replace(oldid, newid);
		}

		protected virtual string GetCompleteInputName()
		{
			if (InputBuilder.ExplicitInputName != null)
				return InputBuilder.ExplicitInputName;

			return InputBuilder.PropertyInfo.Name;
		}

		protected virtual string GetCompleteInputId()
		{
			if (InputBuilder.ExplicitInputId != null)
				return InputBuilder.ExplicitInputId;

			return InputBuilder.PropertyInfo.Name;
		}

		protected virtual object GetValue()
		{
			var model = InputBuilder.Helper.ViewData.Model;
			if (model == null) return null;

			var value = InputBuilder.UseExplicitValue ? InputBuilder.ExplicitValue :  model.GetPropertyValue(GetCompleteInputName());

			return value;
		}

		protected virtual void AppendCleaner(StringBuilder output)
		{
			if (_inputBuilder.AttachCleaner)
				output.Append("<div class='cleaner'></div>");
		}

		protected virtual void AppendValidationError(StringBuilder output, bool isInvalid)
		{
			var invalidAltText = GetLabel();
			if(InputBuilder.InputIndex != null)
			{
				invalidAltText += " " + InputBuilder.InputIndex;
			}

			if (isInvalid)
				output.AppendFormat(
					@"<img src=""/images/icons/exclamation.png"" alt=""Validation error on '{0}'"" class=""errorIndicator"" />",
					invalidAltText);
		}

		protected virtual void AppendLabel(StringBuilder output, string propertyName, string labelClass, bool isRequired)
		{
			if (UseSpanAsLabel)
				output.AppendFormat(@"<span class=""label"">");
			else
			{
				var style = InputBuilder.RenderLabel ? "" : "display: none";
				output.AppendFormat(@"<label for=""{0}"" class=""{1}"" style=""{2}"">", propertyName, labelClass, style);
			}

			if (isRequired)
			{
				output.Append(@"<span class=""requiredFieldIndicator"">*</span>");
			}

			output.AppendFormat(@"{0}:", GetLabel());

			if (UseSpanAsLabel)
				output.Append("</span>");
			else
				output.Append("</label>");
		}

		protected abstract string CreateInputElementBase();

		private string GetLabel()
		{
			if (InputBuilder.PropertyInfo.HasCustomAttribute<BetterValidateNonEmptyAttribute>())
			{
				return InputBuilder.PropertyInfo.GetCustomAttribute<BetterValidateNonEmptyAttribute>().Label;
			}

			if (InputBuilder.PropertyInfo.HasCustomAttribute<BetterValidateDateAttribute>())
			{
				return InputBuilder.PropertyInfo.GetCustomAttribute<BetterValidateDateAttribute>().Label;
			}

			if (InputBuilder.PropertyInfo.HasCustomAttribute<BetterValidateDateTimeAttribute>())
			{
				return InputBuilder.PropertyInfo.GetCustomAttribute<BetterValidateDateTimeAttribute>().Label;
			}

			if (InputBuilder.PropertyInfo.HasCustomAttribute<BetterValidateEmailAttribute>())
			{
				return InputBuilder.PropertyInfo.GetCustomAttribute<BetterValidateEmailAttribute>().Label;
			}

			if (InputBuilder.PropertyInfo.HasCustomAttribute<BetterValidateDecimalAttribute>())
			{
				return InputBuilder.PropertyInfo.GetCustomAttribute<BetterValidateDecimalAttribute>().Label;
			}

			if (InputBuilder.PropertyInfo.HasCustomAttribute<LabelAttribute>())
			{
				return InputBuilder.PropertyInfo.GetCustomAttribute<LabelAttribute>().Value;
			}

			return InputBuilder.PropertyInfo.Name.ToSeparatedWords();
		}

		private string GetExampleText()
		{
			if (InputBuilder.PropertyInfo.HasCustomAttribute<ValidateDecimalAttribute>())
				return "1234.56, 12,234.00, 12345";

			return null;
		}
	}
}