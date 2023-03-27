using CIPLATFORM.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Respository.Interface;
using CIPLATFORM.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;

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
                TempData["loginerror"] = "Invalid Email Or Password.Plz Verify..!!";
                return View();
            }
            HttpContext.Session.SetString("Uname", user.FirstName + " " + user.LastName);


            HttpContext.Session.SetInt32("UId", (Int32)user.UserId);
            return RedirectToAction("HomeGrid", "Platform");

          
        }

        public IActionResult forgot()
        {
            return View();
        }

        [HttpPost]
      
        public IActionResult forgot(ForgotPwd obj)
        {
            User user1 = new User();
            {

                user1.Email = obj.Email;


            }
            if (ModelState.IsValid)
            {
                var user = _UserRepository.forgot(user1);
                if (user == null)
                {
                    TempData["msg"] = "Invalid Email";
                    return View();
                }
                TempData["Message"] = "Check your email to reset password";
                return RedirectToAction("Login");
            }
            return View();
        }

   

        public IActionResult newpassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult newpassword(ResetPwd obj, string token)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                {

                    user.Password = obj.Password;


                }
                var validToken = _UserRepository.newpassword(user, token);

                if (validToken != null)
                {
                    TempData["Message"] = "Your Password is changed";
                    return RedirectToAction("Login");
                }
                TempData["Message"] = "Token not Found";
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult register(Register obj)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                {
                    user.FirstName = obj.FirstName;
                    user.LastName = obj.LastName;
                    user.Email = obj.Email;
                    user.Password = obj.Password;
                    user.PhoneNumber = obj.PhoneNumber;
                }
                var check = _UserRepository.register(user);
                if (check != null)
                {
                    TempData["unsuccess"] = "User already exist";
                  
                    return View();
                }
                else
                {
                    TempData["success"] = "Registration Successfull";
                    return RedirectToAction("login", "User");
                }
            }
            return View();
        }






    }
}