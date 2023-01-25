using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Data;
using WebApplicationMVC.Interface;
using WebApplicationMVC.ViewModels;

namespace WebApplicationMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _context;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _context = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userRaces = await _context.GetAllUserRaces();
            var userClubs = await _context.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
    }
}
