using System.Collections.Generic;
using CodeCampServer.DependencyResolution;
using MvcContrib.UI.InputBuilder.Conventions;

namespace CodeCampServer.Infrastructure.UI.InputBuilders
{
	public class InputBuilderPropertyFactory : List<IPropertyViewModelFactory>
	{
		public InputBuilderPropertyFactory()
		{
			Add(new GuidPropertyConvention());
			Add(new DatePickerPropertyConvention());
			Add(DependencyRegistrar.Resolve(typeof (UserPickerPropertyConvention)) 
			    as IPropertyViewModelFactory);
			Add(new EnumerationPropertyConvention());
			Add(new PasswordPropertyConvention());
			Add(new NullableGuidPropertyConvention());
			Add(new MultiLinePropertyConvention());
			Add(new InputBuilderPropertyConvention());
		}
	}
}