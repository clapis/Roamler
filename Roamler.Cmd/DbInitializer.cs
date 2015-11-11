using System;
using System.IO;
using System.Text.RegularExpressions;
using Roamler.Data.EntityFramework;
using Roamler.Model;
using System.Collections.Generic;

namespace Roamler.Cmd
{
    /// <summary>
    /// Just a quick and dirty helper to load data from CSV file into Database
    /// </summary>
    public class DbInitializer
    {
        private readonly ILocationProvider _provider;

        public DbInitializer(ILocationProvider provider)
        {
            _provider = provider;
        }

        public void Init()
        {
            int count = 0;
            int batch = 10000;

            var db = NewContext();

            foreach (var location in _provider.GetLocations())
            {
                db.Locations.Add(location);
                Console.Write("\r Inserted: {0}", ++count); 

                if (count % batch == 0)
                {
                    db.SaveChanges();
                    db.Dispose();
                    db = NewContext();
                }
            }

            Console.WriteLine("\r Inserted: {0}.", ++count); 
            db.SaveChanges();
            db.Dispose();
        }

        private RoamlerDbContext NewContext()
        {
            var db = new RoamlerDbContext();

            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            return db;
        }
    }

    public interface ILocationProvider
    {
        IEnumerable<Location> GetLocations();
    }

    public class RandomLocationProvider : ILocationProvider
    {
        private readonly Random _random = new Random();

        public IEnumerable<Location> GetLocations()
        {
            for (int i = 0; i < 200000; i++)
            {
                var address = string.Format("{0} Random Address", i);
                var latitude = (_random.NextDouble() * 180) - 90;
                var longitude = (_random.NextDouble() * 360) - 180;

                var location = new Location
                {
                    Address = address,
                    Coordinates = new GeoCoordinate(latitude, longitude)
                };

                yield return location;
            }
        }
    }

    public class CsvLocationProvider : ILocationProvider
    {
        const string CsvPattern = @"""\s*,\s*""";

        private readonly string _filePath;

        public CsvLocationProvider(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ApplicationException("file not found: " + filePath);

            _filePath = filePath;

        }

        public IEnumerable<Location> GetLocations()
        {
            foreach (var line in File.ReadLines(_filePath))
            {
                yield return Parse(line);
            }
        }

        private Location Parse(string input)
        {
            string[] tokens = Regex.Split(input.Substring(1, input.Length - 2), CsvPattern);

            var address = tokens[0];
            var latitude = double.Parse(tokens[1]);
            var longitude = double.Parse(tokens[2]);

            var geoCoodinate = new GeoCoordinate(latitude, longitude);

            return new Location { Address = address, Coordinates = geoCoodinate };
        }

    }
}
