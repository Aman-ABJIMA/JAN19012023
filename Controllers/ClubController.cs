using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using WebApplicationMVC.Data;
using WebApplicationMVC.Interface;
using WebApplicationMVC.Models;
using WebApplicationMVC.ViewModels;

namespace WebApplicationMVC.Controllers
{

    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository; //It provides data from data base
        private readonly IPhotoService _photoService;

        //  private readonly IClubRepository _clubRepository;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            // _clubRepository = clubRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()//c
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();//m
            return View(clubs);//v
        }
        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVm.Image);
                var club = new Club
                {
                    Title = clubVm.Title,
                    Description = clubVm.Description,
                    Image = result.Url.ToString(),
                    Address= new Address
                    {
                        Street= clubVm.Address.Street,
                        City = clubVm.Address.City,
                        State = clubVm.Address.State

                    }
                };
                _clubRepository.Add(club);
       
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed!");
            }
            return View(clubVm);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if(club==null) return View(null);
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                Url = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed To Edit Club");
                return View("Edit", clubVM);
            }
            var userclub = await _clubRepository.GetByIdAsyncNoTracking(id);
            if (userclub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userclub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo!");
                    return View(clubVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address
                };
                _clubRepository.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                    return View(clubVM);
            }
        }
           

         
       
           
       
    }
}
