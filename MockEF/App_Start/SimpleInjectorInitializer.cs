[assembly: WebActivator.PostApplicationStartMethod(typeof(MockEF.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace MockEF.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using MockEF.Data;
    using MockEF.Data.Repository;
    using MockEF.Service;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);

            container.Register<IService, MockEFService>(Lifestyle.Scoped);
            container.Register<IDbContext, Context>(Lifestyle.Singleton);

            container.Register(typeof(IReadWriteRepository<>), typeof(GenericEfRepository<>), Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);


        }
    }
}