using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Data;
using WebApplicationMVC.Models;
using WebApplicationMVC.ViewModels;

namespace WebApplicationMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM )
        {
            if(!ModelState.IsValid) return View(loginVM);
            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if(user != null) {

                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if(passwordCheck)
                {
                    //Password Correct Sign In
                    var result = await _signInManager.PasswordSignInAsync(user,loginVM.Password,false,false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Wrong Credentials,Please Try again!";
                return View(loginVM);
            }
            //User not Found
            TempData["Error"] = "Wrong Credentials,Please Try again!";
            return View(loginVM);
        }


        public IActionResult Register()
        {
            var resposne = new RegisterViewModel();
            return View(resposne);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }
            var newUser = new AppUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, Role.User);
              
            }
            return View("Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Race");
        }
    }
}
