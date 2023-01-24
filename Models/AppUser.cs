using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationMVC.Models
{
    public class AppUser : IdentityUser
    {
        //[Key]
        //public int Id { get; set; }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Race> Races { get; set; }
        public ICollection<Club> Clubs { get; set; }
    }
}
