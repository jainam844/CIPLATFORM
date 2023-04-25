using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using CIPLATFORM.Respository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPLATFORM.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {

        public readonly IProfileRepository _ProfileRepository;
        public readonly IPlatformRepository _PlatformRepository;

        public ProfileController(IProfileRepository ProfileRepository, IPlatformRepository PlatformRepository)
        {
            _ProfileRepository = ProfileRepository;
            _PlatformRepository = PlatformRepository;

        }

        public IActionResult Profile()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;



            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;
            List<City> Cities = new List<City>();

            if (name != null)
            {
                int? UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;

                ProfileViewModel pm = _ProfileRepository.getUser(@ViewBag.UId);

                if(pm.CountryId != null)
                {
                    Cities = _PlatformRepository.GetCitys().Where(x => x.CountryId == pm.CountryId).ToList();
                }
                //else
                //{
                //    Cities = _PlatformRepository.GetCitys(); 
                //}
                ViewBag.Cities = Cities;
                return View(pm);
            }


            return View();
        }

        [HttpPost]
        public IActionResult Profile(ProfileViewModel obj, int save)
        {
            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;

            List<City> Cities = _PlatformRepository.GetCitys();
            ViewBag.Cities = Cities;


            string? name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string? avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int? UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }


            if (save == 1)
            {

                if (obj.resetPass.Password == null || obj.resetPass.ConfirmPassword == null || obj.resetPass.OldPassword == null)
                {
                    TempData["false"] = "All fields are required! Plz Enter Value For All 3 Fields";
                }
                else if (obj.resetPass.ConfirmPassword != obj.resetPass.Password)
                {
                    TempData["morefalse"] = "Password & ConfirmPassword is Diffrent";
                }
                else
                {

                    bool resetpass = _ProfileRepository.changepassword(obj, @ViewBag.UId);
                    obj.skills = _ProfileRepository.getUser(@ViewBag.UId).skills;
                    obj.userSkills = _ProfileRepository.getUser(@ViewBag.UId).userSkills;
                    if (resetpass)
                    {
                        TempData["true"] = "password updated";
                    }
                    else
                    {
                        TempData["false"] = "entered password is wrong";
                    }
                }

                return RedirectToAction("Profile");
            }

            if (save == 3)
            {
                bool saveprofile = _ProfileRepository.saveProfile(obj, @ViewBag.UId);
                //if (obj.Avatarfile != null)
                //{
                //    HttpContext.Session.SetString("Avatar", obj.Avatarfile.FileName);
                //}

                //HttpContext.Session.SetString("Uname", obj.FirstName + " " + obj.LastName);

                obj = _ProfileRepository.getUser(@ViewBag.UId);
                if (saveprofile)
                {
                    TempData["true"] = "Profile Updated Successfully";
                    HttpContext.Session.SetString("Uname", obj.FirstName + " " + obj.LastName);
                    HttpContext.Session.SetInt32("UId", (Int32)obj.UserId);
                    if (obj.Avatar != null)
                    {
                        HttpContext.Session.SetString("Avatar", obj.Avatar);
                    }
                    else
                    {
                        HttpContext.Session.SetString("Avatar", "");
                    }
                }

                else
                {
                    TempData["false"] = "User does not exist";
                }

                //string avtar1 = HttpContext.Session.GetString("Avatar");
                //ViewBag.Avtar = avtar1;

                //string name1 = HttpContext.Session.GetString("Uname");
                //ViewBag.Uname = name1;

                return RedirectToAction("Profile");
            }
            if (save == 4)

            {
                if (obj.contactus.Name == null || obj.contactus.Message == null || obj.contactus.Email == null || obj.contactus.subject == null)
                {
                    TempData["false"] = "All fields are required! Plz Enter Value For All 4 Fields";
                }
                else
                {

                    bool ContactUs = _ProfileRepository.ContactUs(obj);

                    if (ContactUs)
                    {
                        TempData["true"] = "Your Mail Has Been Sent";
                    }
                    else
                    {
                        TempData["false"] = "Error occured during the process";
                    }


                }
                obj.skills = _ProfileRepository.getUser(@ViewBag.UId).skills;
                obj.userSkills = _ProfileRepository.getUser(@ViewBag.UId).userSkills;
                return RedirectToAction("Profile");
            }
            return View(obj);

        }



        public IActionResult Timesheet()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;
            if (name != null)
            {
                int? UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;

            }

            ProfileViewModel pm = _ProfileRepository.GetTimsheet(@ViewBag.UId);
            return View(pm);

        }


        [HttpPost]
        public IActionResult Timesheet(ProfileViewModel obj, int tid)
        {
            string? name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string? avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }

            DateTime today = DateTime.Today;
            if (obj.Timesheet.DateVolunteereed > today)
            {
                TempData["error"] = "Date is invalid";
            }
            else
            {


                bool b = _ProfileRepository.updatetimesheet(obj, tid, ViewBag.UId);
                if (b)
                    TempData["true"] = "Activity added successfully";
                else
                    TempData["false"] = "Activity updated successfully";
            }
            ProfileViewModel pm = _ProfileRepository.GetTimsheet(@ViewBag.UId);
            return RedirectToAction("Timesheet");
        }
        public IActionResult getActivity(int tid)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");

            ProfileViewModel tm = _ProfileRepository.GetActivity(tid, UserId);
            return PartialView("_TimeCard", tm);
        }

        public IActionResult getGoalActivity(int tid)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");

            ProfileViewModel tm = _ProfileRepository.GetActivity(tid, UserId);


            return PartialView("_GoalCard", tm);

        }


        public IActionResult DeleteActivity(int tid)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            bool tm = _ProfileRepository.deletetimesheet(tid);
            if (tm)
                TempData["delete"] = "Activity deleted successfully";
            else
                TempData["delete"] = "Activity cannot deleted";
            return RedirectToAction("Timesheet");

        }
    }
}
