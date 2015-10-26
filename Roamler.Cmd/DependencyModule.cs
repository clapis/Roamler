using System.Linq;
using Autofac;
using Roamler.Data;
using Roamler.Data.EntityFramework;
using Roamler.Model;
using Roamler.Services;
using Roamler.SpatialSearch;
using Roamler.SpatialSearch.Linear;
using Roamler.SpatialSearch.QuadTree;

namespace Roamler.Cmd
{
    public class DependencyModule
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // db
            builder.RegisterType<RoamlerDbContext>();

            // repositories
            builder.RegisterType<LocationRepository>().As<ILocationRepository>();

            // services
            builder.RegisterType<LocationSearchService>().As<ILocationSearchService>();

            // search engine
            //builder.RegisterType<SpatialIndexBuilder>().As<ISpatialIndexBuilder>();
            //builder.RegisterType<LinearSpatialIndexBuilder>().As<ISpatialIndexBuilder>();

            // hack! to resolve SpatialIndexBuilder
            //builder.Register(c => c.Resolve<RoamlerDbContext>().Locations).As<IQueryable<ISpatialDocument>>();

            return builder.Build();
        }

    }

}
