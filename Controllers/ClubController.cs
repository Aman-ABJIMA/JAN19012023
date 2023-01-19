using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Data;

namespace WebApplicationMVC.Controllers
{
      
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(ApplicationDbContext context)//c
        {
            var clubs = _context.Clubs.ToList();//m
            return View();//v
        }
    }
}
