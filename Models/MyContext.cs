using Microsoft.EntityFrameworkCore;

namespace SpaceCrabs.Models
{
    public class MyContext :DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        public DbSet<Crab> Crabs { get; set; }

        public DbSet<Planet> Planets { get; set; }

        public DbSet<Trip>  Trips { get; set; }
    }
}