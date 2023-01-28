using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Data;
using WebApplicationMVC.Interface;
using WebApplicationMVC.Models;
using WebApplicationMVC.ViewModels;

namespace WebApplicationMVC.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IDashboardRepository _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor,IPhotoService photoService)
        {
            _context = dashboardRepository;
            _httpContext= httpContextAccessor;
            _photoService = photoService;
        }
        private void MapUserEdit(AppUser user,EditUserViewModel editVM,ImageUploadResult uploadResult )
        {
            user.Id = editVM.Id;
            user.Pace = editVM.Pace;
            user.Mileage= editVM.Mileage;
            user.ProfileImageUrl= uploadResult.Url.ToString();
            user.City= editVM.City;
            user.State= editVM.State;

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

        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContext.HttpContext.User.GetUserId();
            AppUser user = await _context.GetUserById(currentUserId);
            if (user == null)
                return View("Error");
            var editUserViewModel = new EditUserViewModel()
            {
                Id = currentUserId,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State= user.State,

            };
            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserViewModel editUserViewModel)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Faild to edit profile");
                return View("EditUserProfile", editUserViewModel);
            }
            AppUser user = await _context.GetByIdNoTracking(editUserViewModel.Id);
            if(user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editUserViewModel.Image);
                MapUserEdit(user,editUserViewModel,photoResult);
                _context.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Could not Delete photo!");
                    return View();
                }
                var photoResult = await _photoService.AddPhotoAsync(editUserViewModel.Image);
                MapUserEdit(user,editUserViewModel,photoResult);
                _context.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
