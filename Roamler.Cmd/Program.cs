using System;
using System.Diagnostics;
using Autofac;
using Roamler.Model;
using Roamler.SpatialSearch;
using Roamler.SpatialSearch.Queries;

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
                    var index = BuildIndex(scope);

                    var amsterdam = new GeoCoordinate(52.3667, 4.900);

                    var query = new KnnQuery
                    {
                        Coordinate = amsterdam,
                        MaxDistance = 10000,
                        MaxResults = 15
                    };
                    
                    var timer = Stopwatch.StartNew();
                    Console.Write("Searching top {0} within {1} m from [{2}] ..", query.MaxResults, query.MaxDistance, query.Coordinate);
                    
                    var queryResult = index.KnnSearch(query);
                    
                    timer.Stop();
                    Console.WriteLine("ok ({0} ms)", timer.ElapsedMilliseconds);


                    Console.WriteLine("Found {0} documents", queryResult.Results.Count);

                    foreach(var r in queryResult.Results)
                        Console.WriteLine("{0:N0} m - [{1}]", r.Distance, r.Document.Coordinates);

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


        private static ISpatialIndex BuildIndex(ILifetimeScope scope)
        {
            Console.Write("Building index..");

            var timer = Stopwatch.StartNew();

            var index = scope.Resolve<ISpatialIndex>();

            timer.Stop();

            Console.WriteLine("ok ({0} ms)", timer.ElapsedMilliseconds);

            return index;
        }

    }
    

    
}
