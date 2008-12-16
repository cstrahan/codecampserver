using System;
using System.Collections.Generic;

namespace CodeCampServer.UI.Models.AutoMap
{
	public class ValueFormatter : IValueFormatter, ICustomFormatterExpression
	{
		private readonly IList<IValueFormatter> _formatters = new List<IValueFormatter>();
		private readonly IDictionary<Type, ValueFormatter> _typeSpecificFormatters = new Dictionary<Type, ValueFormatter>();
		private readonly IList<Type> _formattersToSkip = new List<Type>();

		public void AddFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter
		{
			var formatter = (IValueFormatter) Activator.CreateInstance(typeof(TValueFormatter), true);

			AddFormatter(formatter);
		}

		public void AddFormatter(IValueFormatter valueFormatter)
		{
			_formatters.Add(valueFormatter);
		}

	    public void AddFormatExpression(ValueFormatterExpression formatExpression)
	    {
	        throw new System.NotImplementedException();
	    }

	    public void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter
		{
			_formattersToSkip.Add(typeof(TValueFormatter));
		}

		public IFormatterExpression ForSourceType<TSourceType>()
		{
			var valueFormatter = new ValueFormatter();

			_typeSpecificFormatters[typeof (TSourceType)] = valueFormatter;

			return valueFormatter;
		}

		string IValueFormatter.FormatValue(object value, ResolutionContext context)
		{
			Type valueType = context.ModelMemberType;
			string formattedValue = null;
			object valueToFormat = value;
			ValueFormatter typeSpecificFormatter = null;

			if (context.PropertyMap.GetFormatters().Length > 0)
			{
				foreach (IValueFormatter formatter in context.PropertyMap.GetFormatters())
				{
					formattedValue = formatter.FormatValue(valueToFormat, context);
					valueToFormat = formattedValue;
				}
			}
			else if (_typeSpecificFormatters.TryGetValue(valueType, out typeSpecificFormatter))
			{
				if (!context.PropertyMap.FormattersToSkipContains(typeSpecificFormatter.GetType()))
				{
					formattedValue = ((IValueFormatter) typeSpecificFormatter).FormatValue(value, context);
					valueToFormat = formattedValue;
				}
				else
				{
					formattedValue = value.ToNullSafeString();
				}
			}
			else
			{
				formattedValue = value.ToNullSafeString();
			}

			foreach (IValueFormatter formatter in _formatters)
			{
				Type formatterType = formatter.GetType();
				if (CheckPropertyMapSkipList(context, formatterType) &&
					CheckTypeSpecificSkipList(typeSpecificFormatter, formatterType))
				{
					formattedValue = formatter.FormatValue(valueToFormat, context);
					valueToFormat = formattedValue;
				}
			}

			return formattedValue;
		}

		private bool CheckTypeSpecificSkipList(ValueFormatter valueFormatter, Type formatterType)
		{
			if (valueFormatter == null)
			{
				return true;
			}

			return !valueFormatter._formattersToSkip.Contains(formatterType);
		}

		private bool CheckPropertyMapSkipList(ResolutionContext context, Type formatterType)
		{
			return !context.PropertyMap.FormattersToSkipContains(formatterType);
		}

		private class ValueFormatterUsingExpression : IValueFormatter
		{
			private readonly ValueFormatterExpression _valueFormatterExpression;

			public ValueFormatterUsingExpression(ValueFormatterExpression valueFormatterExpression)
			{
				_valueFormatterExpression = valueFormatterExpression;
			}

			public string FormatValue(object value, ResolutionContext context)
			{
				return _valueFormatterExpression(value, context);
			}
		}
	}
}