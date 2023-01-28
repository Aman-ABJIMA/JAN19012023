using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using WebApplicationMVC.Helpers;
using WebApplicationMVC.Interface;
using WebApplicationMVC.Models;
using WebApplicationMVC.Repository;
using WebApplicationMVC.ViewModels;

namespace WebApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPinfo();
            var homeViewModel = new HomeViewModel();
            try
            {
                string url = "https://ipinfo.io?token=691671276012ad";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPinfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City=ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                if (homeViewModel.City != null) ;
                {
                   
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}