using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationMVC.Data;
using WebApplicationMVC.Data.Enums;
using WebApplicationMVC.Interface;
using WebApplicationMVC.Models;
using WebApplicationMVC.Repository;

namespace WebApplicationMVC.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository context)
        {
            _raceRepository = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();

            return View(races);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }
            _raceRepository.Add(race);
            return RedirectToAction("Index");
        }
    }
}
