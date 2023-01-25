using WebApplicationMVC.Models;

namespace WebApplicationMVC.Interface
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
    }
}