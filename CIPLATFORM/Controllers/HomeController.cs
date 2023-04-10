

using CIPLATFORM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CIPLATFORM.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        ////private CiPlatformContext _CiPlatformDb;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }

            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;


            return View();
        }


  


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


