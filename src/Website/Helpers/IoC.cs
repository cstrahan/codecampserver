using System;
using Castle.Windsor;

namespace CodeCampServer.Website.Helpers
{
    /// <summary>
    /// A simple service locator.
    /// </summary>
    public class IoC
    {
        private static IWindsorContainer _container;

        public static void InitializeWith(IWindsorContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        private static void EnsureInitialized()
        {
            if (_container == null) throw new InitializationException("You must initialize IoC before using it.");
        }

        public static T Resolve<T>()
        {
            EnsureInitialized();
            return Resolve<T>(typeof (T).Name);
        }

        private static T Resolve<T>(string key)
        {
            return (T)_container[key];
        }

        public static void Register<Interface, Implementation>()
        {
            Register<Interface, Implementation>(typeof(Interface).Name);
        }

        public static void Register<Interface, Implementation>(string key)
        {
            EnsureInitialized();
            _container.AddComponent(key, typeof(Interface), typeof(Implementation));
        }

        public static void Reset()
        {
            _container.Dispose();
            _container = null;            
        }
    }
}