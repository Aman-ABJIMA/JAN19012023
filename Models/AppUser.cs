﻿using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public int Pace { get; set; }
        public int Mileage { get; set; }
        public Address? Address { get; set; }
        public ICollection<Race> Races { get; set; }
        public ICollection<Club> Clubs { get; set; }
    }
}
