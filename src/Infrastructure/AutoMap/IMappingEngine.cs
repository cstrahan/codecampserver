using System;

namespace CodeCampServer.Infrastructure.AutoMap
{
	public interface IMappingEngine
	{
		TDestination Map<TSource, TDestination>(TSource source);
		object Map(object source, Type sourceType, Type destinationType);
	}
}