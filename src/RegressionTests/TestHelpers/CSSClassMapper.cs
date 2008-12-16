using System;
using System.Linq.Expressions;
using CodeCampServer.UI;
using CodeCampServer.UI.Models.AutoMap;

namespace RegressionTests.TestHelpers
{
    public class CSSClassMapper<TDTO>
    {
        public string Wrap(Expression<Func<TDTO, object>> memberExpr)
        {
            return ReflectionHelper.FindDtoProperty(memberExpr).Name.ToLowerCamelCase();
        }
    }
}