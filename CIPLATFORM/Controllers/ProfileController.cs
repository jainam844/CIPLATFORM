using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Respository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPLATFORM.Controllers
{
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

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }

            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;

            List<City> Cities = _PlatformRepository.GetCitys();
            ViewBag.Cities = Cities;


            return View();
        }

        public JsonResult GetCitys(int countryId)
        {
            List<City> city = _PlatformRepository.GetCityData(countryId);
            var json = JsonConvert.SerializeObject(city);
            return Json(json);
        }

    }
}
