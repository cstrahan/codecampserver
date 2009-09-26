using System;
using System.ComponentModel;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Binders;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
    [TypeConverter(typeof(UserFormTypeConverter))]
    public class UserSelector
    {
        [Required("Name")]
        public virtual string Name { get; set; }

        [Hidden]
        public Guid Id { get; set; }

        [Required("Username")]
        [ValidateRegExp(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",
            "Username is not valid.")]
        public virtual string Username { get; set; }

    }
}