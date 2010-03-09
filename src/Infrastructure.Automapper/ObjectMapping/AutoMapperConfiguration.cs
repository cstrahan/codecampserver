using System;
using System.Collections.Generic;
using AutoMapper;
using CodeCampServer.Core;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Helpers.ActionResults;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping
{
	public class AutoMapperConfiguration : IRequiresConfigurationOnStartup
	{
		public static Func<Type, object> CreateDependencyCallback = (type) => Activator.CreateInstance(type);


		public static void Configure()
		{
			Mapper.Initialize(x =>
			                  	{
			                  		x.ConstructServicesUsing(type => CreateDependencyCallback(type));
			                  		GetProfiles().ForEach(type => x.AddProfile((Profile) Activator.CreateInstance(type)));
			                  	});
		}

		private static IEnumerable<Type> GetProfiles()
		{
			foreach (Type type in typeof (AutoMapperConfiguration).Assembly.GetTypes())
			{
				if (!type.IsAbstract && typeof (Profile).IsAssignableFrom(type))
					yield return type;
			}
		}

		void IRequiresConfigurationOnStartup.Configure()
		{
			Configure();

			AutoMappedViewResult.Map = (a, b, c) => AutoMappedWrapper.Map(a, b, c);

		}
	}
}