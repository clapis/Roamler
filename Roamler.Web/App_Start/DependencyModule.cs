using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using Roamler.Data;
using Roamler.Data.EntityFramework;
using Roamler.Model;
using Roamler.Search;
using Roamler.Search.QuadTree;
using Roamler.Services;

namespace Roamler.Web
{
    public static class DependencyModule
    {
        public static void ConfigureDependencies(this IAppBuilder app, HttpConfiguration config)
        {
            var container = BuildContainer();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container); ;

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // db
            builder.RegisterType<RoamlerDbContext>().InstancePerRequest();

            // repositories
            builder.RegisterType<LocationRepository>().As<ILocationRepository>();

            // services
            builder.RegisterType<LocationSearchService>().As<ILocationSearchService>();

            // controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // search engine
            builder.RegisterType<SpatialIndexBuilder>().As<ISpatialIndexBuilder>();
            builder.Register(c => BuildIndex(c)).As<ISpatialIndex>().SingleInstance();

            return builder.Build();
        }

        // HACK: Build index on first attempt to resolve it
        private static ISpatialIndex BuildIndex(IComponentContext c)
        {
            using (var db = new RoamlerDbContext())
            {
                var builder = c.Resolve<ISpatialIndexBuilder>();

                var locations = db.Locations.Take(100000);

                return builder.BuildIndex(locations);
            }
        }
    }
}