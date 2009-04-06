using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
    public class UserInputBuilder : BaseInputBuilder
    {
        private readonly IUserRepository _repository;

        public UserInputBuilder(IUserRepository repository)
        {
            _repository = repository;
        }


        public override bool IsSatisfiedBy(IInputSpecification specification)
        {
            return (typeof (IEnumerable<UserForm>)).IsAssignableFrom(specification.PropertyInfo.PropertyType);
        }

        protected override string CreateInputElementBase()
        {
            var elementMarkup = new StringBuilder();


            var items = new List<SelectListItem>();
            foreach (User user in _repository.GetAll())
            {
                IEnumerable<UserSelector> value = GetValue() as IEnumerable<UserSelector>;
                bool isChecked = value != null && (value).Where(form => form.Id == user.Id).Count() > 0;
                items.Add(new SelectListItem(){Selected = isChecked,Text = user.Name,Value = user.Id.ToString()});
                //string checkboxName = InputSpecification.InputName + "[" + user.Id + "]";
                //elementMarkup.Append(@"<label for=""" + checkboxName + @""">" + user.Name +
                //                     InputSpecification.Helper.CheckBox(checkboxName, isChecked) + "</label>");
            }
            InputSpecification.Helper.ViewData[InputSpecification.InputName ] = items.Where(item => item.Selected).Select(item => item.Value).ToList();
            return InputSpecification.Helper.ListBox(InputSpecification.InputName,items);
        }
    }
}