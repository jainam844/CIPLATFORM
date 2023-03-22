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

        public PlatformController(CiPlatformContext CiPlatformContext, IPlatformRepository PlatformRepository)
        {
            _PlatformRepository = PlatformRepository;
            _CiPlatformContext = CiPlatformContext;
        }

        /*Home-page*/
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

            var totalMission = _PlatformRepository.GetMissionCount();
            ViewBag.totalMission = totalMission;



            CardsViewModel ms = _PlatformRepository.getCards();

            int pageSize = 2;


            ms.missions = ms.missions.Skip((1 - 1) * pageSize).Take(pageSize).ToList();

            return View(ms);
        }
        public JsonResult GetCitys(int countryId)
        {
            List<City> city = _PlatformRepository.GetCityData(countryId);
            var json = JsonConvert.SerializeObject(city);
            return Json(json);
        }
        public IActionResult Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort, int pageIndex)
        {
            List<Mission> cards = _PlatformRepository.Filter(cityId, countryId, themeId, skillId, search, sort, pageIndex);
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

        //Mission-listing page

        public IActionResult MissionListing(int mid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }



            MissionListingViewModel ml = _PlatformRepository.GetCardDetail(mid);

            return View(ml);

        }

        [HttpPost]
        public bool AddMissionToFavourite(int missionId)
        {
            var userId = (int)HttpContext.Session.GetInt32("UId");

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

        [HttpPost]
        public void AddComment(int obj, string comnt)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            _PlatformRepository.addComment(obj, UserId, comnt);

        }


        [HttpPost]
        public bool applyMission(int missionId)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            var apply = _PlatformRepository.applyMission(missionId, UserId);
            if (apply == true)
            {

                return apply;
            }

            return false;
        }

        public void RecommandToCoWorker(List<int> toUserId, int mid)
        {
            int FromUserId = (int)HttpContext.Session.GetInt32("UId");

            _PlatformRepository.RecommandToCoWorker(FromUserId, toUserId, mid);

            MissionListingViewModel volunteerModel = _PlatformRepository.GetCardDetail(mid);

        }
        public IActionResult nomission()
        {
            return View();
        }
        //page-3
        public IActionResult StoryListing()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;


            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }

            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;
            List<City> Cities = _PlatformRepository.GetCitys();
            ViewBag.Cities = Cities;
            List<MissionTheme> themes = _PlatformRepository.GetMissionThemes();
            ViewBag.themes = themes;

            List<MissionSkill> skills = _PlatformRepository.GetSkills();
            ViewBag.skills = skills;


            StoryListingViewModel sl = _PlatformRepository.GetStoryDetail();
            return View(sl);

        }

        //page-4
        public IActionResult ShareStory()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            return View();
        }
    }
}
