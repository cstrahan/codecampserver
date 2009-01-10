using System;

namespace CodeCampServer.Infrastructure.AutoMap
{
	public interface IConfiguration
	{
		TypeMap[] GetAllTypeMaps();
		TypeMap FindTypeMapFor(Type sourceType, Type destinationType);
		TypeMap FindTypeMapFor<TSource, TDestination>();
		IValueFormatter GetValueFormatter();
		IValueFormatter GetValueFormatter(string profileName);
	}
}