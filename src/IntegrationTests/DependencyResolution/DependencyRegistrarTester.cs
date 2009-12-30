using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl;
using CodeCampServer.UI.Binders;
using CodeCampServer.UI.Controllers;
using NBehave.Spec.NUnit;

namespace CodeCampServer.IntegrationTests.DependencyResolution
{
	using NUnit.Framework;

	
		[TestFixture]
		public class DependencyRegistrarTester
		{

			[Test]
			public void Reflection_helper_can_resolve_repositories()
			{				
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(ConferenceRepository), typeof(IKeyedRepository<>)).ShouldNotBeNull();

				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(ConferenceRepository), typeof(IRepository<>)).ShouldNotBeNull();
			}

			[Test]
			public void Should_register_conferencerepository()
			{
				DependencyRegistrar.EnsureDependenciesRegistered();
				var repository = DependencyRegistrar.Resolve<IConferenceRepository>();
				repository.ShouldBeInstanceOfType(typeof(ConferenceRepository));
			}

			[Test]
			public void Should_register_all_objects()
			{
				DependencyRegistrar.EnsureDependenciesRegistered();
				IEnumerable<Type> controllers = GetControllers();
				foreach (Type controller in controllers)
				{
					DependencyRegistrar.Resolve(controller);
				}
			}

			[Test]
			public void Should_resolve_a_complex_type_for_the_Irepository()
			{
				DependencyRegistrar.EnsureDependenciesRegistered();

				Type repositoryType = typeof(IRepository<>).MakeGenericType(typeof(Conference));

				var binder = (IRepository<Conference>)DependencyRegistrar.Resolve(repositoryType);

				binder.ShouldBeAssignableFrom(typeof(ConferenceRepository));
			}

			[Test]
			public void Should_resolve_a_complex_type_for_the_IKeyed_repository()
			{
				DependencyRegistrar.EnsureDependenciesRegistered();
				Type repositoryType = typeof(IKeyedRepository<>).MakeGenericType(typeof(Conference));
				Type modelBinderType = typeof(KeyedModelBinder<,>).MakeGenericType(typeof(Conference), repositoryType);

				var binder = (IModelBinder)DependencyRegistrar.Resolve(modelBinderType);
				
				binder.ShouldNotBeNull();
			}

			private IEnumerable<Type> GetControllers() {
				Type[] types = typeof(HomeController).Assembly.GetTypes();
				return types.Where(e => IsAController(e));
			}

		private bool IsAController(Type e)
		{				
			return e.GetInterface("IController")!=null&& !e.IsAbstract;
		}
			
		}
	}