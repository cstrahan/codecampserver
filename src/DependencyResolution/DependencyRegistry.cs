using System;
using Castle.Components.Validator;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using AutoMapper;

namespace CodeCampServer.DependencyResolution
{
	public class DependencyRegistry : Registry
	{
		protected override void configure()
		{
			string name = GetType().Assembly.GetName().Name;
			name = name.Substring(0, name.LastIndexOf("."));

			Scan(x =>
			     	{
			     		x.Assembly(name + ".Core");
			     		x.Assembly(name + ".Infrastructure");
			     		x.Assembly(name + ".UI");
			     		x.With<DefaultConventionScanner>();
					
			     	});
			
			
			ForRequestedType<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();

			
		}
	}


	

	public class CastleValidatorRegistry : Registry
	{
		protected override void configure()
		{
			ForRequestedType<IValidatorRunner>().TheDefault.Is.ConstructedBy(
				() => new ValidatorRunner(new CachedValidationRegistry()));
		}
	}

	public class AutoMapperRegistry : Registry
	{
		protected override void configure()
		{
			ForRequestedType<IMappingEngine>().TheDefault.Is.ConstructedBy(() => AutoMapper.Mapper.Engine);
		}
	}
}