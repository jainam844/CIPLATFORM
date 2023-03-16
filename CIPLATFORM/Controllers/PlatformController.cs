using CIPLATFORM.Entities.Data;
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
        public readonly CiPlatformContext _CiPlatformContext;
        //public PlatformController(IPlatformRepository PlatformRepository)
        //{
        //    _PlatformRepository = PlatformRepository;
        //}

        public PlatformController(CiPlatformContext CiPlatformContext, IPlatformRepository PlatformRepository)
        {
            _PlatformRepository = PlatformRepository;
            _CiPlatformContext = CiPlatformContext;
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
        [HttpPost]
        public bool AddMissionToFavourite(int missionId)
        {
            var userId = (int)HttpContext.Session.GetInt32("userid");
            var fav = _PlatformRepository.addToFav(missionId, userId);
            if (fav != true)
            {
                _CiPlatformContext.SaveChanges();
            }
            else
            {
                _CiPlatformContext.SaveChanges();

            }
            return fav;
        }

        public IActionResult MissionListing(int mid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;


            MissionListingViewModel ml = _PlatformRepository.GetCardDetail(mid);


            return View(ml);



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
            return PartialView("_FilterMission", platformModel);

        }


        public JsonResult GetCitys(int countryId)
        {
            List<City> city = _PlatformRepository.GetCityData(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }


        public IActionResult nomission()
        {
            return View();
        }
    }
}
