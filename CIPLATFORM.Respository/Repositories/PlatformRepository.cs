using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Respository.Repositories
{
 public class PlatformRepository : IPlatformRepository { 
    
        public readonly CiPlatformContext _CiPlatformContext;

        public PlatformRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }


        public List<Country> GetCountryData()
        {
            List<Country> country=_CiPlatformContext.Countries.ToList();
            return country;
        }
        public List<City> GetCityData(int countryId)
        {
            List<City>city=_CiPlatformContext.Cities.Where(i => i.CountryId == countryId).ToList(); 
            return city;
        }

        public List<MissionTheme> GetMissionThemes()
        {
            List<MissionTheme> themes = _CiPlatformContext.MissionThemes.ToList();
            return themes;

        }

        public List<Skill> GetSkills()
        {
            List<Skill> skills = _CiPlatformContext.Skills.ToList();
            return skills;

        }


        public List<Mission> GetMissions()
        {

            var missions = _CiPlatformContext.Missions.ToList();
            return missions;

        }
        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _CiPlatformContext.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.MissionMedia).ToList();
            return missionDetails;
        }
       public int GetMissionRatings(long missionID)
        {
            MissionRating rating= _CiPlatformContext.MissionRatings.FirstOrDefault(a=>a.MissionId==missionID);
            return rating.Rating;
        }


    }
}
