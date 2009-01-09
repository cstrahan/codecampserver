using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.UI.Helpers.ViewPage;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class InputBuilder : IInputBuilder
	{
		private readonly PropertyInfo _propertyInfo;
		private readonly HtmlHelper _helper;
		private readonly IInputBuilderFactory _factory;
		private bool _inline;
		private object _attributes;
		private string _explicitInputName;
		private object _explicitValue;
		private bool _useExplicitValue;
		private bool _renderLabel = true;
		private bool _attachCleaner = true;
		private string _explicitInputId;
		private int? _inputIndex;

		public string ExplicitInputId
		{
			get { return _explicitInputId;  }
		}

		public int? InputIndex
		{
			get { return _inputIndex; }
		}

		public string ExplicitInputName
		{
			get { return _explicitInputName; }
		}

		public object ExplicitValue
		{
			get { return _explicitValue; }
		}

		public bool UseExplicitValue
		{
			get { return _useExplicitValue; }	
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

		public object Attributes
		{
			get { return _attributes; }
		}

		public HtmlHelper Helper
		{
			get { return _helper; }
		}

		public bool UseSpanAsLabel
		{
			get { return false; }
		}

		public bool AttachCleaner
		{
			get { return _attachCleaner; }
		}

		public InputBuilder(PropertyInfo info, HtmlHelper helper, IInputBuilderFactory factory)
		{
			_propertyInfo = info;
			_helper = helper;
			_factory = factory;
		}

		public override string ToString()
		{
			BaseInputCreator creator = _factory.CreateInputCreator(this);

			return creator.CreateInputElement();
		}

		public InputBuilder WithInputName(string name)
		{
			_explicitInputName = name;
			return this;
		}

		public InputBuilder WithNoLabel()
		{
			_renderLabel = false;
			return this;
		}

		public InputBuilder FromValue(object value)
		{
			_useExplicitValue = true;
			_explicitValue = value;
			return this;
		}

		public InputBuilder DisplayedInline()
		{
			_inline = true;
			return this;
		}

		public InputBuilder WithAttributes(object attributes)
		{
			_attributes = attributes;
			return this;
		}

		public InputBuilder WithId(string id)
		{
			_explicitInputId = id;
			return this;
		}

		public InputBuilder WithInputIndex(int index)
		{
			_inputIndex = index;
			return this;
		}

		public InputBuilder WithNoCleaner()
		{
			_attachCleaner = false;
			return this;
		}
	}
}