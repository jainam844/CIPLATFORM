using CIPLATFORM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Respository.Interface
{
   public interface IPlatformRepository
    {
        public List<Country> GetCountryData();
        public List<City> GetCityData(int countryId);


        public List<MissionTheme> GetMissionThemes();

        public List<Skill> GetSkills();
      
        public List<Mission> GetMissions();
        public List<Mission> GetMissionDetails();
        public int GetMissionRatings(long missionID);
    }
}
