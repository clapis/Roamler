using System.Collections.Generic;
using Roamler.Model;

namespace Roamler.Data
{
    public interface ILocationRepository
    {
        List<Location> FindIn(params int[] ids);
    }
}
