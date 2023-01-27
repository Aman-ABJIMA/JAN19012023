using WebApplicationMVC.Data.Enums;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.ViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int AddressId { get; set; }
        public string Url { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }

    }
}
