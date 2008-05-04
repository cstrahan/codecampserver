using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using CodeCampServer.Model;
using MvcContrib.Castle;
using MvcContrib.ControllerFactories;

namespace CodeCampServer.Website
{
    public class Global : HttpApplication, IContainerAccessor
    {
        public const string WindsorConfigFilename = @"Windsor.config.xml";

        private IWindsorContainer _container;
        private static readonly object _lockDummy = new object();
        private bool _initiliazed;

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

        private void InitializeWindsor()
        {
            var configInterpreter = new XmlInterpreter(WindsorConfigFilename);
            _container = new WindsorContainer(configInterpreter);
            initializeComponents();

            
            ControllerBuilder.Current.SetControllerFactory(typeof(WindsorControllerFactory));

            _initiliazed = true;
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            Log.EnsureInitialized();
            InitializeWindsor();
                        
            setupRoutes();
        }

        private void initializeComponents()
        {            
            _container.AddComponent("route-configurator", typeof (IRouteConfigurator), typeof (RouteConfigurator));                                    

            //add all controllers
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (typeof(IController).IsAssignableFrom(type))
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