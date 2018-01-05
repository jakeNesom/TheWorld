using Microsoft.Extensions.Logging;
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
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, 
            ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All Trips from the Database");
            return _context.Trips.ToList();
        }
    }
}
