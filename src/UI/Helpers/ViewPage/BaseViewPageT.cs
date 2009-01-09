using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.UI.Models.AutoMap;
using CodeCampServer.UI.ViewPage;
using StructureMap;
using Tarantino.Core.Commons.Services.Security;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class BaseViewPage<TModel> : ViewPage<TModel> where TModel : class
	{
		private readonly IInputBuilderFactory _inputBuilderFactory;
		private readonly ISecurityContext _securityContext;
		private readonly IDisplayErrorMessages _displayErrorMessages;

		public BaseViewPage()
		{
			_inputBuilderFactory = ObjectFactory.GetInstance<IInputBuilderFactory>();
			_securityContext = ObjectFactory.GetInstance<ISecurityContext>();
			_displayErrorMessages = ObjectFactory.GetInstance<IDisplayErrorMessages>();
		}

		protected virtual ISecurityContext SecurityContext
		{
			get { return _securityContext; }
		}

		public IDisplayErrorMessages Errors
		{
			get
			{
				_displayErrorMessages.ModelState = ViewData.ModelState;
				return _displayErrorMessages;
			}
		}

		protected IInputBuilder InputFor(Expression<Func<TModel, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.FindDtoProperty(expression);

			return new InputBuilder(property, Html, _inputBuilderFactory);
		}

		protected IInputBuilder InputFor<T>(Expression<Func<T, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.FindDtoProperty(expression);

			return new InputBuilder(property, Html, _inputBuilderFactory);
		}

	}
}