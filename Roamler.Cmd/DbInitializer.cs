using System;
using System.IO;
using System.Text.RegularExpressions;
using Roamler.Data.EntityFramework;
using Roamler.Model;

namespace Roamler.Cmd
{
    /// <summary>
    /// Just a quick and dirty helper to load data from CSV file into Database
    /// </summary>
    public static class DbInitializer
    {
        const string CsvPattern = @"""\s*,\s*""";

        public static void LoadLocationsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ApplicationException("file not found: " + filePath);
            
            
            var db = NewContext();

            int count = 0;

            foreach (var line in File.ReadLines(filePath))
            {
                var location = Parse(line);
                db.Locations.Add(location);
                Console.Write("\r Inserted: {0}", ++count); 

                if (count % 1000 == 0)
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

        private static RoamlerDbContext NewContext()
        {
            var db = new RoamlerDbContext();

            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            return db;
        }

        private static Location Parse(string input)
        {
            string[] tokens = Regex.Split(input.Substring(1, input.Length - 2), CsvPattern);

            var address = tokens[0];
            var latitude = double.Parse(tokens[1]);
            var longitude = double.Parse(tokens[2]);

            var geoCoodinate = new GeoCoordinate(latitude, longitude);

            return new Location { Address = address, Coordinate = geoCoodinate };
        }
    }
}
