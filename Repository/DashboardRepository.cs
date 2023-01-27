using WebApplicationMVC.Data;
using WebApplicationMVC.Interface;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public DashboardRepository(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = httpContextAccessor;
        }

        

        public async Task<List<Club>> GetAllUserClubs()
        {
            var currentUser = _HttpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs =  _context.Clubs.Where(a => a.AppUser.Id == currentUser);
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currentUser = _HttpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces = _context.Races.Where(a => a.AppUser.Id == currentUser);
            return userRaces.ToList();
        }
    }
}
