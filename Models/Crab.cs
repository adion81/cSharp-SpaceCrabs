using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceCrabs.Models
{
    public class Crab
    {
        [Key]
        public int CrabId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Weapon { get; set; }

        [Required]
        public string SpaceCraft { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //NOT stored in Database
        public List<Trip> Trips { get; set; }
    }
}