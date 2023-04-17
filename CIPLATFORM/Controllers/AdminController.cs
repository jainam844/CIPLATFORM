using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using CIPLATFORM.Respository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPLATFORM.Controllers
{
    public class AdminController : Controller
    {
        public readonly IAdminRepository _AdminRepository;





        public AdminController(IAdminRepository AdminRepository)
        {
            _AdminRepository = AdminRepository;

        }

        public IActionResult Admin()
        {
            AdminViewModel am = _AdminRepository.getData();
            ViewBag.Totalpages = Math.Ceiling(am.users.Count() / 5.0);
            am.users = am.users.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            return View(am);
        }
        public IActionResult UserFilter(string? search,int pg)
        {

            List<User> fusers = _AdminRepository.Usersearch(search,pg);

            AdminViewModel am = new AdminViewModel();
            {
                am.users = fusers;
            }

            ViewBag.pg_no = pg;
            ViewBag.Totalpages = Math.Ceiling(_AdminRepository.Usersearch(search, 0).Count() / 5.0);
            am.users = fusers.Skip((1 - 1) * 5).Take(5).ToList();



            return PartialView("_User", am);
        }

    }
}
