using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.Core.Common;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class InputSpecification : IInputSpecificationExpression, IInputSpecification
	{
		private readonly PropertyInfo _propertyInfo;
		private readonly HtmlHelper _helper;
		private readonly IInputBuilderFactory _factory;
		private readonly UrlHelper _urlHelper;
		private bool _inline;
		private object _customAttributes;
		private bool _renderLabel = true;
		private bool _attachCleaner = true;
		private bool _invalidOption;
		private readonly LambdaExpression _expression;
		private readonly LambdaExpression _parentExpression;
		private readonly string _inputName;
		private readonly int? _inputIndex;
		private readonly string _inputId;
		public Type InputBuilderType { get; private set; }

		public string InputId
		{
			get { return _inputId; }
		}

		public bool Inline
		{
			get { return _inline; }
		}

		public bool RenderLabel
		{
			get { return _renderLabel; }
		}

		public PropertyInfo PropertyInfo
		{
			get { return _propertyInfo; }
		}

		public object CustomAttributes
		{
			get { return _customAttributes; }
		}

		public HtmlHelper Helper
		{
			get { return _helper; }
		}

		public UrlHelper Url
		{
			get { return _urlHelper; }
		}

		public bool UseSpanAsLabel
		{
			get { return false; }
		}

		public bool AttachCleaner
		{
			get { return _attachCleaner; }
		}

		public bool InvalidOption
		{
			get { return _invalidOption; }
		}

		public string InputName
		{
			get { return _inputName; }
		}

		public int? InputIndex
		{
			get { return _inputIndex; }
		}

		public LambdaExpression Expression
		{
			get { return _expression; }
		}

		public LambdaExpression ParentExpression
		{
			get { return _parentExpression; }
		}

		public InputSpecification(HtmlHelper helper, IInputBuilderFactory factory, UrlHelper urlHelper,
		                          LambdaExpression expression, LambdaExpression parentExpression)
		{
			_helper = helper;
			_factory = factory;
			_urlHelper = urlHelper;
			_expression = expression;
			_parentExpression = parentExpression;

			_propertyInfo = ReflectionHelper.FindProperty(expression);
			_inputIndex = UINameHelper.ExtractIndexValueFrom(_expression);

			string inputName = UINameHelper.BuildNameFrom(_expression);
			string inputId = UINameHelper.BuildIdFrom(_expression);

			if (parentExpression != null)
			{
				inputName = UINameHelper.BuildNameFrom(parentExpression) + inputName;
				inputId = UINameHelper.BuildIdFrom(parentExpression) + inputId;
			}

			_inputName = inputName;
			_inputId = inputId;
		}

		public override string ToString()
		{
			IInputBuilder builder = _factory.FindInputBuilderFor(this);

			return builder.Build(this);
		}

		public IInputSpecificationExpression NoLabel()
		{
			_renderLabel = false;
			return this;
		}

		public IInputSpecificationExpression DisplayedInline()
		{
			_inline = true;
			return this;
		}

		public IInputSpecificationExpression Attributes(object attributes)
		{
			_customAttributes = attributes;
			return this;
		}

		public IInputSpecificationExpression NoCleaner()
		{
			_attachCleaner = false;
			return this;
		}

		public IInputSpecificationExpression Using<T>() where T : IInputBuilder
		{
			InputBuilderType = typeof (T);
			return this;
		}

		public IInputSpecificationExpression WithInvalidOption()
		{
			_invalidOption = true;
			return this;
		}
	}
}