using Microsoft.EntityFrameworkCore;
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

        public void AddStop(string tripName, Stop newStop, string username)
        {
            var trip = GetUserTripByName(tripName, username);

            if( trip != null)
            {
                // in first method call, the foreign kep is added to the stop
                // in the second call, the stop is added as a new object
                // need both things to happen so stop is saved correctly and related to the other objects correctly.
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All Trips from the Database");
            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            // entity framework lambda expressions - 
            return _context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name == tripName)
                .FirstOrDefault();
        }

        public IEnumerable<Trip> GetTripsByUsername(string name)
        {
            return _context
                .Trips
                .Include(t => t.Stops)
                .Where(t => t.UserName == name)
                .ToList();
                
        }

        public Trip GetUserTripByName(string tripName, string username)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name == tripName && t.UserName == username)
                .FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        { 
            // save changes async can return the number of 
            // db rows affected.
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
