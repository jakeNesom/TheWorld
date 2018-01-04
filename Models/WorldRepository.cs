using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        // an 'interface' we can use with testing etc so that we don't have to re-write queries themselves
        // repository can be 'mocked up' for testing so we can test a controller against a fake repository
        // world repository is a way of 'hiding' the DbContext db accessor methods
        // instead of exposing the dB accessors, we are hiding them in methods here
        // then we instantiate a copy of world repository in the startup.cs and inejct it into the app controller
        private WorldContext _context;

        public WorldRepository(WorldContext context)
        {
            _context = context;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.Trips.ToList();
        }
    }
}
