using CIPLATFORM.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Respository.Interface;

namespace CIPLATFORM.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserRepository _UserRepository;
        //public readonly CiPlatformContext _CiPlatformDb;


        public UserController(IUserRepository UserRepository)
        {
           _UserRepository = UserRepository;
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(User obj)
        {
            var user=_UserRepository.login(obj);
           if(user==null)
            {
                return View();
            }
        
            return RedirectToAction("register", "User");
        }

        public IActionResult forgot()
        {
            return View();
        }

        [HttpPost]
      
        public IActionResult forgot(User obj)
        {
            var user = _UserRepository.forgot(obj);
            if (user == null)
            {
                TempData["Message"] = "Invalid Email";
                return View();
            }
            TempData["Message"] = "Check your email to reset password";
            return RedirectToAction("Login");
        }

        public IActionResult newpassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult newpassword(User obj, string token)
        {

            var validToken = _UserRepository.newpassword(obj, token);

            if (validToken != null)
            {
                TempData["Message"] = "Your Password is changed";
                return RedirectToAction("Login");
            }
            TempData["Message"] = "Token not Found";
            return RedirectToAction("Login");
        }

        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult register(User obj)
        {
            var user = _UserRepository.register(obj);
            if (user != null)
            {
                return View();
            }
            return RedirectToAction("login");
        }

    }
}