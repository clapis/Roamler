using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autofac;
using Roamler.Data.EntityFramework;
using Roamler.Model;
using Roamler.Search;
using Roamler.Search.QuadTree;
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

                    var locations = LoadLocations(db).AsQueryable();

                    var index = BuildIndex(locations);

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



        private static List<Location> LoadLocations(RoamlerDbContext db)
        {
            var timer = Stopwatch.StartNew();

            Console.Write("Loading data.. ");

            var locations = db.Locations.ToList();

            timer.Stop();

            Console.WriteLine("ok ({0} ms)", timer.ElapsedMilliseconds);

            return locations;
        }

        private static ISpatialIndex BuildIndex(IQueryable<Location> locations)
        {
            Console.Write("Building index.. ");

            var builder = new SpatialIndexBuilder(locations);

            var timer = Stopwatch.StartNew();

            var index = builder.BuildIndex();

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
