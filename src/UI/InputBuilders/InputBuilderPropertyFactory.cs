using System;
using System.Collections.Generic;
using MvcContrib.UI.InputBuilder.Conventions;

namespace CodeCampServer.UI.InputBuilders
{
	public class InputBuilderPropertyFactory : List<IPropertyViewModelFactory>
	{
		public static Func<Type, object> CreateDependencyCallback = (type) => Activator.CreateInstance(type);

		public T CreateDependency<T>()
		{
			return (T)CreateDependencyCallback(typeof(T));
		}
		
		public InputBuilderPropertyFactory()
		{
			Add(new GuidPropertyConvention());
			Add(new DatePickerPropertyConvention());
			Add(CreateDependencyCallback(typeof (UserPickerPropertyConvention)) 
			    as IPropertyViewModelFactory);
			Add(new EnumerationPropertyConvention());
			Add(new PasswordPropertyConvention());
			Add(new NullableGuidPropertyConvention());
			Add(new MultiLinePropertyConvention());
			Add(new EmailPropertyConvention());
			Add(new IgnoredEntityPropertyConvention());
			Add(new SelectListProvidedPropertyConvention());
			Add(new InputBuilderPropertyConvention());
		}
	}
}