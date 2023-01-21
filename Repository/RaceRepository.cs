using Microsoft.EntityFrameworkCore;
using WebApplicationMVC.Data;
using WebApplicationMVC.Interface;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Repository
{
    public class RaceRepository : IRaceRepository
    {
        public RaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public readonly AppDbContext _context;

        public bool Add(Race race)
        {
          _context.Add(race); return Save();  
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            var race = await _context.Races.ToListAsync();
            return race;
        }

        public async Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City == city).ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i=>i.Address).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
