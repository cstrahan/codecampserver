using System.Reflection;
using CodeCampServer.UI.Views;

namespace Cuc.Jcms.UI.ViewPage
{
    public class InputElementAuthority : IInputElementAuthority
    {
        #region IInputElementAuthority Members

        public bool Permits(PropertyInfo propertyInfo)
        {
            return true;
        }

        public bool Forbids(PropertyInfo propertyInfo)
        {
            return !Permits(propertyInfo);
        }

        #endregion
    }
}