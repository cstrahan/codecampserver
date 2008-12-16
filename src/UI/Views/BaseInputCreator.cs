using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Castle.Components.Validator;
using Cuc.Jcms.Core;
using Cuc.Jcms.Core.Domain;
using Cuc.Jcms.Core.Domain.Model;
using Cuc.Jcms.Core.Domain.Model.Enumerations;
using Cuc.Jcms.UI.Models.AutoMap;
using Cuc.Jcms.UI.Models.Forms.Attributes;
using Cuc.Jcms.UI.Models.Validation.Attributes;
using StructureMap;
using Tarantino.Core.Commons.Model.Enumerations;

namespace Cuc.Jcms.UI.Views
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

            var propertyName = InputBuilder.PropertyInfo.Name;

            bool isRequired = InputBuilder.PropertyInfo.HasCustomAttribute<ValidateNonEmptyAttribute>();
            bool isInvalid = false;

            if (InputBuilder.Helper.ViewData.ModelState.ContainsKey(propertyName) &&
                InputBuilder.Helper.ViewData.ModelState[propertyName].Errors.Count > 0)
                isInvalid = true;

            var labelClass = InputBuilder.Inline ? "nestedLabel" : "";

            var output = new StringBuilder();

            if (InputBuilder.RenderLabel)
                AppendLabel(output, propertyName, labelClass, isRequired);

            output.Append(CreateInputElementBase());

            AppendValidationError(output, isInvalid);

            AppendCleaner(output);

            var exampleText = GetExampleText();
            if (!string.IsNullOrEmpty(exampleText))
                output.AppendFormat("<span class=\"example\">ex.: {0}</span>", exampleText);

            return output.ToString();
        }

        protected virtual string GetCompleteInputName()
        {
            if (InputBuilder.ExplicitInputName != null)
                return InputBuilder.ExplicitInputName;

            return InputBuilder.PropertyInfo.Name;
        }

        protected virtual object GetValue()
        {
            var model = InputBuilder.Helper.ViewData.Model;
            if (model == null) return null;

            var value = InputBuilder.ExplicitValue ?? model.GetPropertyValue(GetCompleteInputName());

            return value;
        }

        protected virtual void AppendCleaner(StringBuilder output)
        {
            output.Append("<div class='cleaner'></div>");
        }

        protected virtual void AppendValidationError(StringBuilder output, bool isInvalid)
        {
            if (isInvalid)
                output.AppendFormat(
                    @"<img src=""/images/icons/exclamation.png"" alt=""Validation error on '{0}'"" class=""errorIndicator"" />", GetLabel());
        }

        protected virtual void AppendLabel(StringBuilder output, string propertyName, string labelClass, bool isRequired)
        {
            if (UseSpanAsLabel)
                output.AppendFormat(@"<span class=""label"">");
            else
                output.AppendFormat(@"<label for=""{0}"" class=""{1}"">", propertyName, labelClass);

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

    public class TextBoxInputBuilder : BaseInputCreator
    {
        public TextBoxInputBuilder(InputBuilder inputBuilder)
            : base(inputBuilder)
        {
        }

        protected override string CreateInputElementBase()
        {
            return InputBuilder.Helper.TextBox(GetCompleteInputName(), null, InputBuilder.Attributes);
        }
    }

    public class StateDropDownInputBuilder : BaseInputCreator
    {
        public StateDropDownInputBuilder(InputBuilder inputBuilder)
            : base(inputBuilder)
        {
        }

        protected override string CreateInputElementBase()
        {
            return InputBuilder.Helper.StateDropDown(GetCompleteInputName(), InputBuilder.Attributes);
        }
    }

    public class UserDropDownBuilder : BaseInputCreator
    {
        public UserDropDownBuilder(InputBuilder inputBuilder)
            : base(inputBuilder)
        {
        }

        protected override string CreateInputElementBase()
        {
            var userRepos = ObjectFactory.GetInstance<IUserRepository>();
			var users = new List<User>(userRepos.GetAll());
			users.Insert(0,null);
			
				
            var selectList = new SelectList(users, "Id", "Username",
                                            GetValue());
            return InputBuilder.Helper.DropDownList(GetCompleteInputName(), selectList);
        }
    }

    public class HiddenInputBuilder : BaseInputCreator
    {
        public HiddenInputBuilder(InputBuilder inputBuilder)
            : base(inputBuilder)
        {
        }

        protected override string CreateInputElementBase()
        {
            return InputBuilder.Helper.Hidden(GetCompleteInputName());
        }

        protected override void AppendLabel(StringBuilder output, string propertyName, string labelClass, bool isRequired)
        {
            return;
        }

        protected override void AppendCleaner(StringBuilder output)
        {
            return;
        }
    }


    public class CheckboxInputBuilder : BaseInputCreator
    {
        public CheckboxInputBuilder(InputBuilder inputBuilder)
            : base(inputBuilder)
        {
        }

        protected override string CreateInputElementBase()
        {
            return InputBuilder.Helper.CheckBox(GetCompleteInputName(), InputBuilder.Attributes);
        }
    }

    public class DateInputBuilder : BaseInputCreator
    {
        public DateInputBuilder(InputBuilder inputBuilder)
            : base(inputBuilder)
        {
        }

        protected override string CreateInputElementBase()
        {
            var attributes = MakeDictionary(InputBuilder.Attributes);

            if (attributes.ContainsKey("class"))
            {
                attributes["class"] = attributes["class"] + " date-pick";
            }
            else
            {
                attributes.Add("class", "date-pick");
            }

            return InputBuilder.Helper.TextBox(GetCompleteInputName(), null, attributes);
        }

        private static IDictionary<string, object> MakeDictionary(object withProperties)
        {
            var dic = new Dictionary<string, object>();
            var properties = TypeDescriptor.GetProperties(withProperties);
            foreach (PropertyDescriptor property in properties)
            {
                dic.Add(property.Name, property.GetValue(withProperties));
            }
            return dic;
        }
    }

    public class RadioInputBuilder : BaseInputCreator
    {
        public RadioInputBuilder(InputBuilder inputBuilder)
            : base(inputBuilder)
        {
        }

        protected override bool UseSpanAsLabel
        {
            get { return true; }
        }

        protected override string CreateInputElementBase()
        {
            var builder = new StringBuilder();

            foreach (
                var enumeration in
                    EnumerationHelper.GetAll(InputBuilder.PropertyInfo.PropertyType).Cast<BetterEnumeration>().OrderBy(e => e.DisplayOrder))
            {
                string checkedvalue = (GetSelectedValue() == enumeration.DisplayName) ? "checked=\"checked\"" : "";

                builder.Append(
                    string.Format(
                        "<input id=\"{1}_{0}\" type=\"radio\" value=\"{0}\" name=\"{1}\" {3} /><label class=\"nestedLabel\" for=\"{1}_{0}\">{2}</label>",
                        enumeration.Value, GetCompleteInputName(), enumeration.DisplayName, checkedvalue));
            }

            return builder.ToString();
        }
        private string GetSelectedValue()
        {
            var model = InputBuilder.Helper.ViewData.Model;
            if (model == null) return null;
            var value = model.GetPropertyValue(InputBuilder.PropertyInfo.Name);
            if (value != null) return ((Enumeration)value).DisplayName;
            return null;
        }
    }

    public class EnumerationInputBuilder : BaseInputCreator
    {
        public EnumerationInputBuilder(InputBuilder inputBuilder)
            : base(inputBuilder)
        {
        }

        protected override string CreateInputElementBase()
        {
            var selectList = new SelectList(EnumerationHelper.GetAll(InputBuilder.PropertyInfo.PropertyType), "Value", "DisplayName",
                                            GetSelectedValue());
            return InputBuilder.Helper.DropDownList(GetCompleteInputName(), selectList, InputBuilder.Attributes);
        }

        private object GetSelectedValue()
        {
            var value = GetValue();

            if (value != null) return ((Enumeration)value).Value;
            return null;
        }
    }

    public class NullInputBuilder : BaseInputCreator
    {
        public NullInputBuilder(InputBuilder inputBuilder)
            : base(inputBuilder)
        {
        }

        protected override bool OutputEmpty
        {
            get
            {
                return true;
            }
        }

        protected override string CreateInputElementBase()
        {
            return "";
        }
    }

    internal class YesNoRadioInputBuilder : BaseInputCreator
    {
        public YesNoRadioInputBuilder(InputBuilder builder)
            : base(builder)
        {

        }

        protected override bool UseSpanAsLabel
        {
            get { return true; }
        }

        protected override string CreateInputElementBase()
        {
            var builder = new StringBuilder();

            builder.Append(CreateRadioInput("Yes", GetSelectedValue() == true, true));
            builder.Append(CreateRadioInput("No", GetSelectedValue() == false, false));

            return builder.ToString();
        }

        private string CreateRadioInput(string label, bool selected, bool value)
        {
            string checkedvalue = selected ? "checked=\"checked\"" : "";
            return string.Format(
                "<input id=\"{1}_{0}\" type=\"radio\" value=\"{0}\" name=\"{1}\" {3} /><label class=\"nestedLabel\" for=\"{1}_{0}\">{2}</label>",
                value, GetCompleteInputName(), label, checkedvalue);
        }

        private bool? GetSelectedValue()
        {
            var model = InputBuilder.Helper.ViewData.Model;
            if (model == null) return false;
            var value = model.GetPropertyValue(InputBuilder.PropertyInfo.Name);
            return (bool?)value;
        }
    }
}