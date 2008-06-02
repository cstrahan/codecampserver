using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using CodeCampServer.Model;
using CodeCampServer.Website.Helpers;
using MvcContrib.Castle;
using MvcContrib.ExtendedComponentController;

namespace CodeCampServer.Website
{
    public class Global : HttpApplication, IContainerAccessor
    {
        public const string WindsorConfigFilename = @"bin\Windsor.config.xml";

        private static readonly object _lockDummy = new object();
        private IWindsorContainer _container;
        private bool _initiliazed;

        #region IContainerAccessor Members

        public IWindsorContainer Container
        {
            get
            {
                if (!_initiliazed)
                {
                    lock (_lockDummy)
                    {
                        if (!_initiliazed)
                            InitializeWindsor();
                    }
                }

                return _container;
            }
        }

        #endregion

        private void InitializeWindsor()
        {
            var configInterpreter = new XmlInterpreter(WindsorConfigFilename);
            _container = new WindsorContainer(configInterpreter);
            initializeComponents();
            IoC.InitializeWith(_container);

            ComponentControllerBuilder.Current.SetComponentControllerFactory(
                new IoCComponentControllerFactory(new WindsorDependencyResolver(_container)));

            _initiliazed = true;
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            Log.EnsureInitialized();
            InitializeWindsor();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container));

            setupRoutes();
        }

        private void initializeComponents()
        {
            _container.AddComponent("route-configurator", typeof (IRouteConfigurator), typeof (RouteConfigurator));

            // TODO: windsor mass component registration
            // http://www.kenegozi.com/Blog/2008/03/01/windsor-mass-component-registration.aspx
            // http://mikehadlow.blogspot.com/2008/04/problems-with-mvc-framework.html

            //add all controllers
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (typeof (IController).IsAssignableFrom(type))
                {
                    _container.AddComponentWithLifestyle(type.Name.ToLower(), type, LifestyleType.Transient);
                }
                if (typeof (ComponentController).IsAssignableFrom(type))
                {
                    _container.AddComponentWithLifestyle(type.Name.ToLower(), type, LifestyleType.Transient);
                }
            }
        }

        private void setupRoutes()
        {
            var configurator = _container.Resolve<IRouteConfigurator>();
            configurator.RegisterRoutes();
        }
    }
}