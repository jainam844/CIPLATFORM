﻿using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using CIPLATFORM.Respository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIPLATFORM.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public readonly IAdminRepository _AdminRepository;

        public AdminController(IAdminRepository AdminRepository)
        {
            _AdminRepository = AdminRepository;

        }
        public IActionResult Admin()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }

            AdminViewModel am = _AdminRepository.getData();
            ViewBag.Totalpages1 = Math.Ceiling(am.users.Count() / 5.0);
            am.users = am.users.Skip((1 - 1) * 5).Take(5).ToList();


            ViewBag.Totalpages2 = Math.Ceiling(am.cmspages.Count() / 5.0);
            am.cmspages = am.cmspages.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages3 = Math.Ceiling(am.missions.Count() / 5.0);
            am.missions = am.missions.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages4 = Math.Ceiling(am.missionthemes.Count() / 5.0);
            am.missionthemes = am.missionthemes.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages5 = Math.Ceiling(am.skills.Count() / 5.0);
            am.skills = am.skills.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages6 = Math.Ceiling(am.missionapplications.Count() / 5.0);
            am.missionapplications = am.missionapplications.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;


            ViewBag.Totalpages7 = Math.Ceiling(am.stories.Count() / 5.0);
            am.stories = am.stories.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;



            ViewBag.Totalpages8 = Math.Ceiling(am.banners.Count() / 5.0);
            am.banners = am.banners.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;


            return View(am);
        }
    
        [HttpPost]
        public IActionResult Admin(AdminViewModel obj, int command)
        {
            //var addMsg = "{0} added Suceessfully."
            if (command == 1)
            {
                bool userpage = _AdminRepository.addcms(obj, command);
                if (userpage)
                    TempData["true"] = "User  added Successfully";
                else
                    TempData["false"] = "User  updated Successfully";
            }
            if (command == 2)
            {
                bool addcmspage = _AdminRepository.addcms(obj, command);
                if (addcmspage)
                    TempData["true"] = "CMS Activity added Successfully";
                else
                    TempData["false"] = "CMS Activity updated Successfully";
            }
            if (command == 3)
            {
                bool addcmspage = _AdminRepository.addcms(obj, command);
                if (addcmspage)
                    TempData["true"] = "mission added Successfully";
                else
                    TempData["false"] = "mission updated Successfully";
            }
            if (command == 4)
            {
                bool missiontheme = _AdminRepository.addcms(obj, command);
                if (missiontheme)
                    TempData["true"] = "MissionTheme added Successfully";
                else
                    TempData["false"] = "MissionTheme updated Successfully";
            }
            if (command == 5)
            {
                bool missionskill = _AdminRepository.addcms(obj, command);
                if (missionskill)
                    TempData["true"] = "MissionTheme added Successfully";
                else
                    TempData["false"] = "MissionTheme updated Successfully";
            }
            if (command == 8)
            {
                bool missionskill = _AdminRepository.addcms(obj, command);
                if (missionskill)
                    TempData["true"] = "Banner added Successfully";
                else
                    TempData["false"] = "Banner updated Successfully";
            }

            AdminViewModel am = _AdminRepository.getData();
            ViewBag.Totalpages1 = Math.Ceiling(am.users.Count() / 5.0);
            am.users = am.users.Skip((1 - 1) * 5).Take(5).ToList();


            ViewBag.Totalpages2 = Math.Ceiling(am.cmspages.Count() / 5.0);
            am.cmspages = am.cmspages.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages3 = Math.Ceiling(am.missions.Count() / 5.0);
            am.missions = am.missions.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages4 = Math.Ceiling(am.missionthemes.Count() / 5.0);
            am.missionthemes = am.missionthemes.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages5 = Math.Ceiling(am.skills.Count() / 5.0);
            am.skills = am.skills.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages6 = Math.Ceiling(am.missionapplications.Count() / 5.0);
            am.missionapplications = am.missionapplications.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;


            ViewBag.Totalpages7 = Math.Ceiling(am.stories.Count() / 5.0);
            am.stories = am.stories.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages8 = Math.Ceiling(am.banners.Count() / 5.0);
            am.banners = am.banners.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            return View(am);
        }

        public IActionResult EditForm(int id, string page)
        {
            AdminViewModel am = _AdminRepository.getData();
            ViewBag.Totalpages1 = Math.Ceiling(am.users.Count() / 5.0);
            am.users = am.users.Skip((1 - 1) * 5).Take(5).ToList();


            ViewBag.Totalpages2 = Math.Ceiling(am.cmspages.Count() / 5.0);
            am.cmspages = am.cmspages.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages3 = Math.Ceiling(am.missions.Count() / 5.0);
            am.missions = am.missions.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages4 = Math.Ceiling(am.missionthemes.Count() / 5.0);
            am.missionthemes = am.missionthemes.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages5 = Math.Ceiling(am.skills.Count() / 5.0);
            am.skills = am.skills.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages6 = Math.Ceiling(am.missionapplications.Count() / 5.0);
            am.missionapplications = am.missionapplications.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;


            ViewBag.Totalpages7 = Math.Ceiling(am.stories.Count() / 5.0);
            am.stories = am.stories.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            ViewBag.Totalpages8 = Math.Ceiling(am.banners.Count() / 5.0);
            am.banners = am.banners.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            if (page == "nav-user")
            {
                am.user = _AdminRepository.EditForm(id, page).user;
                if(am.user.CountryId != null)
                {
                    am.cities=_AdminRepository.getData().cities.Where(x=>x.CountryId==am.user.CountryId).ToList();  
                }
                return PartialView("_User", am);
            }

            else if (page == "nav-cms")
            {
                am.CmsPage = _AdminRepository.EditForm(id,page).CmsPage;
                return PartialView("_CMSPages", am);
            }
            else if (page == "nav-mission")
            {
                am.mission = _AdminRepository.EditForm(id, page).mission;
                am.missionthemes = _AdminRepository.getData().missionthemes;
                am.skills = _AdminRepository.getData().skills;
               if(am.mission.CountryId != null)
                {
                    am.cities=_AdminRepository.getData().cities.Where(x=>x.CountryId==am.mission.CountryId).ToList();
                }
                return PartialView("_Mission", am);
            }
            else if (page == "nav-theme")
            {
                am.missionTheme = _AdminRepository.EditForm(id,page).missionTheme;
                return PartialView("_MissionTheme", am);
            }
            else if (page == "nav-skill")
            {
                am.skill = _AdminRepository.EditForm(id, page).skill;
                return PartialView("_MissionSkill", am);
            }
            else if (page == "nav-banner")
            {
                am.banner = _AdminRepository.EditForm(id, page).banner;
                return PartialView("_Banner", am);
            }

            return PartialView("_CMSPages", am);
        }

        public IActionResult DeleteActivity(int id, int page)
        {

            bool tm = _AdminRepository.deleteactivity(id, page);
            if (tm)
                TempData["delete"] = "Activity deleted successfully";
            else
                TempData["delete"] = "Activity cannot deleted";
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            return RedirectToAction("Admin");

        }
        public IActionResult Approval(int id, int page, int status)
        {
            bool tm = _AdminRepository.Approval(id, page, status);
            if (tm)
                TempData["accept"] = "Request Accepted";
            else
                TempData["decline"] = "Request declined";
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            return RedirectToAction("Admin");
        }

        public IActionResult UserFilter(string? search, int pg, string key)
        {

            AdminViewModel x = _AdminRepository.Usersearch(search, 0);
            AdminViewModel fusers = _AdminRepository.Usersearch(search, pg);




            ViewBag.pg_no = pg;
            ViewBag.Totalpages1 = Math.Ceiling(x.users.Count() / 5.0);
            ViewBag.Totalpages2 = Math.Ceiling(x.cmspages.Count() / 5.0);

            ViewBag.Totalpages3 = Math.Ceiling(x.missions.Count() / 5.0);
            ViewBag.Totalpages4 = Math.Ceiling(x.missionthemes.Count() / 5.0);
            ViewBag.Totalpages5 = Math.Ceiling(x.skills.Count() / 5.0);
            ViewBag.Totalpages6 = Math.Ceiling(x.missionapplications.Count() / 5.0);
            ViewBag.Totalpages7 = Math.Ceiling(x.stories.Count() / 5.0);

            ViewBag.Totalpages8 = Math.Ceiling(x.banners.Count() / 5.0);


            if (key == "user")
            {
                return PartialView("_User", fusers);
            }
            if (key == "cms")
            {
                return PartialView("_CMSPages", fusers);
            }
            if (key == "mission")
            {
                return PartialView("_Mission", fusers);
            }
            if (key == "missiontheme")
            {
                return PartialView("_MissionTheme", fusers);
            }
            if (key == "missionskill")
            {
                return PartialView("_MissionSkill", fusers);
            }


            if (key == "missionapplication")
            {
                return PartialView("_MissionApplication", fusers);
            }
            if (key == "story")
            {
                return PartialView("_Story", fusers);
            }
            if (key == "banner")
            {
                return PartialView("_Banner", fusers);
            }
            return View(fusers);

        }

    }
}
