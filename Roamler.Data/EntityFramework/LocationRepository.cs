using System.Collections.Generic;
using System.Linq;
using Roamler.Model;

namespace Roamler.Data.EntityFramework
{
    public class LocationRepository : ILocationRepository
    {
        private readonly RoamlerDbContext _dbContext;

        public LocationRepository(RoamlerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Location> FindIn(params int[] ids)
        {
            return _dbContext.Locations.Where(l => ids.Contains(l.Id)).ToList();
        }
    }
}
