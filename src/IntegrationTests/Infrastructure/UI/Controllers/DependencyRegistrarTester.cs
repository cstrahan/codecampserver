using System;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.Infrastructure.UI.Controllers
{
    [TestFixture]
    public class DependencyRegistrarTester
    {
        private static bool TypeIsAControllerThatShouldBeRegistered(Type type)
        {
            return typeof (IController).IsAssignableFrom(type)
                   && type != typeof (ISubController)
                   && type != typeof (SubController)
                   && type != typeof (SmartController)
                   && !type.IsAbstract;
        }

        [Test]
        public void All_controllers_should_be_resolvable()
        {

            Assembly assembly = GetUiAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (TypeIsAControllerThatShouldBeRegistered(type))
                {
                    Assert.That(DependencyRegistrar.Registered(type), Is.True);
                }
            }
        }

        private Assembly GetUiAssembly()
        {
            return typeof(SmartController).Assembly;
        }

        [Test]
        public void Should_create_everything_that_is_registered()
        {
            DependencyRegistrar.EnsureDependenciesRegistered();
        }

        [Test]
        public void Should_test_the_registered_method()
        {
            Assembly assembly = GetUiAssembly();
        }
    }
}