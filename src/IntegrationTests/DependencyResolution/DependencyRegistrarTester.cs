using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl;
using CodeCampServer.UI.Binders.Keyed;
using CodeCampServer.UI.Controllers;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.DependencyResolution
{
	[TestFixture]
	public class DependencyRegistrarTester
	{
		[Test]
		public void Reflection_helper_can_resolve_repositories()
		{
			ReflectionHelper.IsConcreteAssignableFromGeneric(typeof (ConferenceRepository), typeof (IKeyedRepository<>)).
				ShouldNotBeNull();

			ReflectionHelper.IsConcreteAssignableFromGeneric(typeof (ConferenceRepository), typeof (IRepository<>)).
				ShouldNotBeNull();
		}

		[Test]
		public void Should_register_conferencerepository()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			var repository = DependencyRegistrar.Resolve<IConferenceRepository>();
			repository.ShouldBeInstanceOfType(typeof (ConferenceRepository));
		}

		[Test]
		public void Should_register_all_objects()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			var controllers = GetControllers();
			foreach (var controller in controllers)
			{
				DependencyRegistrar.Resolve(controller);
			}
		}

		[Test]
		public void Should_resolve_a_complex_type_for_the_Irepository()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();

			var repositoryType = typeof (IRepository<>).MakeGenericType(typeof (Conference));

			var binder = (IRepository<Conference>) DependencyRegistrar.Resolve(repositoryType);

			binder.ShouldBeAssignableFrom(typeof (ConferenceRepository));
		}

		[Test]
		public void Should_resolve_a_complex_type_for_the_IKeyed_repository()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			var repositoryType = typeof (IKeyedRepository<>).MakeGenericType(typeof (Conference));
			var modelBinderType = typeof (KeyedModelBinder<,>).MakeGenericType(typeof (Conference), repositoryType);

			var binder = (IKeyedModelBinder) DependencyRegistrar.Resolve(modelBinderType);

			binder.ShouldNotBeNull();
		}

		private IEnumerable<Type> GetControllers()
		{
			var types = typeof (HomeController).Assembly.GetTypes();
			return types.Where(e => IsAController(e));
		}

		private bool IsAController(Type e)
		{
			return e.GetInterface("IController") != null && !e.IsAbstract;
		}
	}
}