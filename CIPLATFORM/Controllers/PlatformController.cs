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

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }

            string avtar = HttpContext.Session.GetString("Avatar");
         ViewBag.Avtar = avtar;

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



            ViewBag.Totalpages = Math.Ceiling(ms.missions.Count() / 3.0);
            ms.missions = ms.missions.Skip((1 - 1) * 3).Take(3).ToList();
            ViewBag.pg_no = 1;


            return View(ms);
        }
        public JsonResult GetCitys(int countryId)
        {
            List<City> city = _PlatformRepository.GetCityData(countryId);
            var json = JsonConvert.SerializeObject(city);
            return Json(json);
        }
        public IActionResult Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort, int pg)
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


            List<Mission> cards = _PlatformRepository.Filter(cityId, countryId, themeId, skillId, search, sort, pg, @ViewBag.UId);
            CardsViewModel platformModel = new CardsViewModel();

            platformModel.missions = cards;
            if (cards.Count == 0)
            {
                return PartialView("_nomission");
            }
            else if (cards.Count >= 1)
            {
                ViewBag.totalMission = _PlatformRepository.Filter(cityId, countryId, themeId, skillId, search, sort, 0, @ViewBag.UId).Count;
            }

            ViewBag.pg_no = pg;
            ViewBag.Totalpages = Math.Ceiling(_PlatformRepository.Filter(cityId, countryId, themeId, skillId, search, sort, 0, @ViewBag.UId).Count / 3.0);
            platformModel.missions = cards.Skip((1 - 1) * 3).Take(3).ToList();

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

            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;

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

        //Star Rating
        [HttpPost]
        public JsonResult MissionRating(int mid, int rating)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            bool success = _PlatformRepository.MissionRating(UserId, mid, rating);
            return Json(success);
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
            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;

            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;
            List<City> Cities = _PlatformRepository.GetCitys();
            ViewBag.Cities = Cities;
            List<MissionTheme> themes = _PlatformRepository.GetMissionThemes();
            ViewBag.themes = themes;

            List<MissionSkill> skills = _PlatformRepository.GetSkills();
            ViewBag.skills = skills;


            StoryListingViewModel sl = _PlatformRepository.GetStoryDetail();



            ViewBag.Totalpages = Math.Ceiling(sl.stories.Count() / 3.0);
            sl.stories = sl.stories.Skip((1 - 1) * 3).Take(3).ToList();
            ViewBag.pg_no = 1;


            return View(sl);

        }
        public IActionResult StoryFilter(string? search, int pg)
        {

            List<Story> cards = _PlatformRepository.StoryFilter(search,pg);

            StoryListingViewModel sModel = new StoryListingViewModel();
            {
                sModel.stories = cards;
            }

            ViewBag.pg_no = pg;
            ViewBag.Totalpages = Math.Ceiling(_PlatformRepository.StoryFilter(search, 0).Count() / 3.0);
            sModel.stories = cards.Skip((1 - 1) * 3).Take(3).ToList();
            return PartialView("_StoryCard", sModel);
        }

        //page-4

        public IActionResult ShareStory()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;


            StoryListingViewModel ss = _PlatformRepository.ShareStory(@ViewBag.UId);

            return View(ss);
        }

        [HttpPost]
        public IActionResult ShareStory(StoryListingViewModel obj, int command, List<IFormFile> file)
        {

            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            bool abc = _PlatformRepository.saveStory(obj, command, @ViewBag.UId);
            bool image = _PlatformRepository.SaveImage(obj, file);
            if (command == 1)
            {
                StoryListingViewModel ss = _PlatformRepository.ShareStory(@ViewBag.UId);

                return View(ss);
            }
            if (command == 2)
            {
                return RedirectToAction("StoryListing", "Platform");
            }
            return View();
        }



        [HttpPost]
        public JsonResult CheckData(int mid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            // Check if the saved data exists in your data store based on the selected option
            var SM = _PlatformRepository.getData(mid, @ViewBag.UId);

            var dataExists = JsonConvert.SerializeObject(SM);

            // Return a boolean value indicating whether the data exists
            return Json(dataExists);

            //return View("~/Story/StoryApply", StoryModel);
        }


        public IActionResult StoryDetail(int sid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;
            StoryListingViewModel sl = _PlatformRepository.GetStory(sid);
            string avtar = HttpContext.Session.GetString("Avatar");
            ViewBag.Avtar = avtar;
            return View(sl);
        }


        public void RecommandStory(List<int> toUserId, int sid)
        {
            int FromUserId = (int)HttpContext.Session.GetInt32("UId");

            _PlatformRepository.RecommandStory(FromUserId, toUserId, sid);

            StoryListingViewModel volunteerModel = _PlatformRepository.GetStory(sid);

        }

    }
}
