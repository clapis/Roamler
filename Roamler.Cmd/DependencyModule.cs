using Autofac;
using Roamler.Data.EntityFramework;
using Roamler.Search;
using Roamler.Search.QuadTree;

namespace Roamler.Cmd
{
    public class DependencyModule
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // db
            builder.RegisterType<RoamlerDbContext>().AsSelf();

            // db init
            builder.RegisterType<DbInitializer>().AsSelf();
            //builder.RegisterType<CsvLocationProvider>().As<ILocationProvider>();
            builder.RegisterType<RandomLocationProvider>().As<ILocationProvider>();

            // index
            builder.RegisterType<SpatialIndexBuilder>().As<ISpatialIndexBuilder>();
                

            return builder.Build();
        }

    }

}
