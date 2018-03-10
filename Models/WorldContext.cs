using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContext : IdentityDbContext<WorldUser>
    {//root class becomes IdentityDbContext when we add Identity to the DB,
        // before root class was DbContext

        private IConfigurationRoot _config;

        // this class is for interfacing with the database
        public WorldContext(IConfigurationRoot config, DbContextOptions options) : base(options) // base() passes parameters to inherited class
        {
            _config = config;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        //overriding a method from DBContext to access the MS Sql server database connection 
        // entity core handles context, there are separate packages for database engines (relational / non relational)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnectionSQLMGTStudio"]);  // were going to pass in the config data we inejcted into the root startup.cs
        }
    }
}
