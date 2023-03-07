using CIPLATFORM.Entities.Models;
using CIPLATFORM.Respository.Interface;
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

            List<MissionTheme> themes = _PlatformRepository.GetMissionThemes();
            ViewBag.themes = themes;

            List<Skill> skills = _PlatformRepository.GetSkills();
            ViewBag.skills = skills;


            List<Mission> missionDeails = _PlatformRepository.GetMissionDetails();
            ViewBag.MissionDeails = missionDeails;



            return View();
        }

       
        public JsonResult GetCity(int countryId)
        {
            List<City> city = _PlatformRepository.GetCityData(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }

        public IActionResult HomeList()
        {
            return View();
        }
    }
}
