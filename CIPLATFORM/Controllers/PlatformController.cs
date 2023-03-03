using Microsoft.AspNetCore.Mvc;

namespace CIPLATFORM.Controllers
{
    public class PlatformController : Controller
    {
        public IActionResult HomeGrid()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            return View();
        }
        public IActionResult HomeList()
        {
            return View();
        }
    }
}
