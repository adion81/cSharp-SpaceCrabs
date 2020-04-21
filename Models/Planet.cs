using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceCrabs.Models
{
    public class Planet
    {
        [Key]
        public int PlanetId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string System { get; set; }

        [Required]
        public string Galaxy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdateAt { get; set; } = DateTime.Now;

        //NOT stored in Database
        public List<Trip> Tours { get; set; }
    }
}