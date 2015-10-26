using Autofac;
using Roamler.Data.EntityFramework;

namespace Roamler.Cmd
{
    public class DependencyModule
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // db
            builder.RegisterType<RoamlerDbContext>();

            return builder.Build();
        }

    }

}
