using System.ComponentModel.DataAnnotations;

namespace SpaceCrabs.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        public int CrabId {get; set;}

        public int PlanetId { get; set; }

        public Crab Tourist { get; set; }

        public Planet Vacation { get; set; }
    }
}