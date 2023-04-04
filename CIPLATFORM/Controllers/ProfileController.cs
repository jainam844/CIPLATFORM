using Microsoft.AspNetCore.Mvc;

namespace CIPLATFORM.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            return View();
        }
    }
}
