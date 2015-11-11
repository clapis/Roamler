using System;
using System.Diagnostics;
using System.Linq;
using Autofac;
using Roamler.Data.EntityFramework;
using Roamler.Model;
using Roamler.Search;
using Roamler.Search.Queries;

namespace Roamler.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var container = DependencyModule.BuildContainer();

                using (var scope = container.BeginLifetimeScope())
                {
                    var db = scope.Resolve<RoamlerDbContext>();
                    var initializer = scope.Resolve<DbInitializer>();
                    var indexBuilder = scope.Resolve<ISpatialIndexBuilder>();

                    // InitializeDatabase(initializer);

                    var locations = db.Locations.Take(200000);

                    var index = BuildIndex(indexBuilder, locations);

                    BenchmarkSearch(index, 100);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Oooops! Something went wrong :(");
                Console.WriteLine(ex);
            }

            Console.Write(Environment.NewLine);
            Console.WriteLine("Program terminated. Press any key to continue..");
            Console.ReadKey();
        }

        private static void InitializeDatabase(DbInitializer initializer)
        {
            var timer = Stopwatch.StartNew();

            Console.Write("Initializing db.. ");

            initializer.Init();

            timer.Stop();

            Console.WriteLine("ok ({0} ms)", timer.ElapsedMilliseconds);
        }

        private static ISpatialIndex BuildIndex(ISpatialIndexBuilder builder, IQueryable<Location> locations)
        {
            Console.Write("Building index.. ");

            var timer = Stopwatch.StartNew();

            var index = builder.BuildIndex(locations);

            timer.Stop();

            Console.WriteLine("ok ({0} ms)", timer.ElapsedMilliseconds);

            return index;
        }

        private static void BenchmarkSearch(ISpatialIndex index, int rounds)
        {
            var query = new KnnQuery
            {
                Coordinate = new GeoCoordinate(52.3667, 4.900),
                MaxDistance = 10000,
                MaxResults = 15
            };

            Console.Write("Searching top {0} within {1} m from [{2}] .. ", query.MaxResults, query.MaxDistance, query.Coordinate);

            var total = 0L;

            var timer = new Stopwatch();

            for (int i = rounds; i > 0; i--)
            {
                timer.Restart();
                index.KnnSearch(query);
                timer.Stop();
                total += timer.ElapsedMilliseconds;
            }

            Console.WriteLine("ok ({0} ms)", total / rounds);
        }

    }
    

    
}
