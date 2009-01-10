using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Common;

namespace CodeCampServer.RegressionTests.TestHelpers
{
	public class CSSClassMapper<TDTO>
	{
		public string Wrap(Expression<Func<TDTO, object>> memberExpr)
		{
			return ReflectionHelper.FindProperty(memberExpr).Name.ToLowerCamelCase();
		}
	}
}