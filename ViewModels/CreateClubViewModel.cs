using Microsoft.AspNetCore.Http;
using WebApplicationMVC.Data.Enums;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.ViewModels
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address  Address { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory ClubCategory { get; set; }

    }
}
