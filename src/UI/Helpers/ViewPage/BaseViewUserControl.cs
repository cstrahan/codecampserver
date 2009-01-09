using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.UI.Helpers.ViewPage;
using CodeCampServer.UI.Models.AutoMap;
using StructureMap;
using Tarantino.Core.Commons.Services.Security;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class BaseViewUserControl : ViewUserControl
	{
		private readonly IInputBuilderFactory _inputBuilderFactory;
		private readonly ISecurityContext _securityContext;

		public BaseViewUserControl()
		{
			_inputBuilderFactory = ObjectFactory.GetInstance<IInputBuilderFactory>();
			_securityContext = ObjectFactory.GetInstance<ISecurityContext>();
		}

		protected ISecurityContext SecurityContext
		{
			get { return _securityContext; }
		}

		protected IInputBuilder InputFor<TDto>(Expression<Func<TDto, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.FindDtoProperty(expression);

			return new InputBuilder(property, Html, _inputBuilderFactory);
		}
	}
}