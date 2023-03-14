using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using CIPLATFORM.Respository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPLATFORM.Controllers
{
    public class PlatformController : Controller
    {
        public readonly IPlatformRepository _PlatformRepository;
        public PlatformController(IPlatformRepository PlatformRepository)
        {
            _PlatformRepository = PlatformRepository;
        }
        public IActionResult HomeGrid()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;



            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;
            List<City> Cities = _PlatformRepository.GetCitys();
            ViewBag.Cities = Cities;
            List<MissionTheme> themes = _PlatformRepository.GetMissionThemes();
            ViewBag.themes = themes;

            List<MissionSkill> skills = _PlatformRepository.GetSkills();
            ViewBag.skills = skills;


            List<Mission> missionDeails = _PlatformRepository.GetMissionDetails();
            ViewBag.MissionDeails = missionDeails;
            //ViewBag.country = _PlatformRepository.GetCountryData();



            //ViewBag.skill = _PlatformRepository.GetSkills();

            //ViewBag.theme = _PlatformRepository.GetMissionThemes();





            var totalMission = _PlatformRepository.GetMissionCount();
            ViewBag.totalMission = totalMission;



            CardsViewModel ms = _PlatformRepository.getCards();


            return View(ms);

          
        }




        public IActionResult Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort)
        {
            List<Mission> cards = _PlatformRepository.Filter(cityId, countryId, themeId, skillId, search, sort);
            CardsViewModel platformModel = new CardsViewModel();
      
                platformModel.missions = cards;
            if (cards.Count == 0)
            {
                return PartialView("_nomission");
            }
            else if (cards.Count >= 1)
            {
                ViewBag.totalMission = cards.Count;
            }
            return PartialView("_GridCard", platformModel);
            
           

        }


        public JsonResult GetCitys(int countryId)
        {
            List<City> city = _PlatformRepository.GetCityData(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }

        public IActionResult HomeList()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            return View();
        }
        public IActionResult nomission()
        {
            return View();
        }
    }
}
