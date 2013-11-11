using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Queryable.EntityFramework;
using Queryable.Web.Infrastructure;
using Queryable.Web.Models;
using Unity.Mvc3;

namespace Queryable.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var repoType = typeof (IRepository<>);


            SetupContainer();
        }

        private void SetupContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<DbContext>(new InjectionFactory(c => new AppContext()));
            container.RegisterType<IRepository<Person>, DbContextRepository<Person>>(new PerHttpRequestLifetimeManager());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

    }
}