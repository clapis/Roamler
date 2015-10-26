using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using Roamler.Search;

namespace Roamler.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // setup DI container
            app.ConfigureDependencies(config);

            // build search index
            BuildSearchIndex(config);

            // setup web api
            config.ConfigureWebApi();
            app.UseWebApi(config);
            
        }

        private void BuildSearchIndex(HttpConfiguration config)
        {
            var scope = config.DependencyResolver.GetRootLifetimeScope();

            // index will be built at first attempt to resolve it
            scope.Resolve<ISpatialIndex>();
        }

    }
}