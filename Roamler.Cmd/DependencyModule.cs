using Autofac;
using Roamler.Data;
using Roamler.Data.EntityFramework;
using Roamler.Services;
using Roamler.SpatialSearch;
using Roamler.SpatialSearch.QuadTree;

namespace Roamler.Cmd
{
    public class DependencyModule
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // db
            builder.RegisterType<RoamlerDbContext>().SingleInstance();

            // repositories
            builder.RegisterType<LocationRepository>().As<ILocationRepository>();

            // services
            builder.RegisterType<LocationSearchService>().As<ILocationSearchService>();

            // search engine
            builder.RegisterType<SpatialIndexBuilder>().As<ISpatialIndexBuilder>();
            builder.Register(c => c.Resolve<ISpatialIndexBuilder>().BuildIndex())
                .As<ISpatialIndex>()
                .SingleInstance();

            return builder.Build();
        }

    }

}
