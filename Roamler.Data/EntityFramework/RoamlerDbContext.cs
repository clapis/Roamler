using System.Data.Entity;
using System.Diagnostics;
using Roamler.Model;

namespace Roamler.Data.EntityFramework
{
    public class RoamlerDbContext : DbContext
    {
        public RoamlerDbContext() : base("Roamler")
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        public DbSet<Location> Locations { get; set; }

    }
}
