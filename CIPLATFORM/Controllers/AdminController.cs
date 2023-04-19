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

            return View(am);
        }

        //public IActionResult Admin(AdminViewModel obj, int command)
        //{


        //    AdminViewModel am = _AdminRepository.getData();
        //    ViewBag.Totalpages1 = Math.Ceiling(am.users.Count() / 5.0);
        //    am.users = am.users.Skip((1 - 1) * 5).Take(5).ToList();


        //    ViewBag.Totalpages2 = Math.Ceiling(am.cmspages.Count() / 5.0);
        //    am.cmspages = am.cmspages.Skip((1 - 1) * 5).Take(5).ToList();
        //    ViewBag.pg_no = 1;

        //    ViewBag.Totalpages3 = Math.Ceiling(am.missions.Count() / 5.0);
        //    am.missions = am.missions.Skip((1 - 1) * 5).Take(5).ToList();
        //    ViewBag.pg_no = 1;

        //    ViewBag.Totalpages4 = Math.Ceiling(am.missionthemes.Count() / 5.0);
        //    am.missionthemes = am.missionthemes.Skip((1 - 1) * 5).Take(5).ToList();
        //    ViewBag.pg_no = 1;

        //    ViewBag.Totalpages5 = Math.Ceiling(am.missionSkills.Count() / 5.0);
        //    am.missionSkills = am.missionSkills.Skip((1 - 1) * 5).Take(5).ToList();
        //    ViewBag.pg_no = 1;

        //    ViewBag.Totalpages6 = Math.Ceiling(am.missionapplications.Count() / 5.0);
        //    am.missionapplications = am.missionapplications.Skip((1 - 1) * 5).Take(5).ToList();
        //    ViewBag.pg_no = 1;


        //    ViewBag.Totalpages7 = Math.Ceiling(am.stories.Count() / 5.0);
        //    am.stories = am.stories.Skip((1 - 1) * 5).Take(5).ToList();
        //    ViewBag.pg_no = 1;
        //    bool addcmspage = _AdminRepository.addcms(obj, command);
        //    AdminViewModel am1 = _AdminRepository.getData();
        //    if (addcmspage)
        //    {
        //        TempData["true"] = "CMS Activity added successfully";
        //    }
        //    return View(am1);

        //}

        [HttpPost]
        public IActionResult Admin(AdminViewModel obj, int command)
        {
            if (command == 2)
            {
                bool addcmspage = _AdminRepository.addcms(obj, command);
                if (addcmspage)
                    TempData["true"] = "CMS Activity added Successfully";
                else
                    TempData["false"] = "CMS Activity updated Successfully";
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

            if (page == "nav-cms")
            {
                am.CmsPage = _AdminRepository.EditForm(id,page).CmsPage;
                return PartialView("_CMSPages", am);
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
            return PartialView("_CMSPages", am);
        }



        public IActionResult DeleteActivity(int id, int page)
        {

            bool tm = _AdminRepository.deleteactivity(id, page);
            if (tm)
                TempData["delete"] = "Activity deleted successfully";
            else
                TempData["delete"] = "Activity cannot deleted";
            return RedirectToAction("Admin");

        }
        public IActionResult Approval(int id, int page, int status)
        {
            bool tm = _AdminRepository.Approval(id, page, status);
            if (tm)
                TempData["accept"] = "Request Accepted";
            else
                TempData["decline"] = "Request declined";
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
            return View(fusers);

        }

    }
}
