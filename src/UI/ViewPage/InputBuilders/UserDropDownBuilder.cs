using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using StructureMap;

namespace CodeCampServer.UI.ViewPage.InputBuilders
{
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
            users.Insert(0, null);


            var selectList = new SelectList(users, "Id", "Username",
                                            GetValue());
            return InputBuilder.Helper.DropDownList(GetCompleteInputName(), selectList);
        }
    }
}