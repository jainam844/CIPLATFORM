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

            return View(am);
        }
    }
}
