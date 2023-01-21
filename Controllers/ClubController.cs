using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationMVC.Data;
using WebApplicationMVC.Interface;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
      
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository; //It provides data from data base
      //  private readonly IClubRepository _clubRepository;

        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
           // _clubRepository = clubRepository;
        }

        public async Task<IActionResult> Index()//c
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();//m
            return View(clubs);//v
        }
        public async Task<IActionResult> Detail(int id)
        {
            Club club =await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Club club)
        {
            if(!ModelState.IsValid)
            {
                return View(club);
            }
            _clubRepository.Add(club);
            return RedirectToAction("Index");
        }
    }
}
