using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
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
        public List<City> GetCitys();
        public List<City> GetCityData(int countryId);


        public List<MissionTheme> GetMissionThemes();


        public List<MissionSkill> GetSkills();
        public List<Mission> GetMissionDetails();


        public CardsViewModel getCards();
        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort,int pg,int UId);
        public int GetMissionCount();

        public MissionListingViewModel GetCardDetail(int mid);
        public List<MissionMedium> media(int mid);
        public bool addToFav(int missionId, int userId);


        //public int avgRating(int mid);
        public List<MissionDocument> document(int mid);
        public void addComment(int mid, int uid, string comnt);
        public bool applyMission(int mid, int uid);
        public void RecommandToCoWorker(int FromUserId, List<int> ToUserId, int mid);
        public bool MissionRating(int userId, int mid, int rating);
        
            public StoryListingViewModel GetStoryDetail();
        public List<StoryMedium> smedia(int sid);
        public StoryListingViewModel GetStory(int sid);
        //public List<Story> GetStoryList(string? search);
        public void RecommandStory(int FromUserId, List<int> ToUserId, int sid);
        public List<Story> StoryFilter(string? search);
        public List<MissionApplication> Mission(int UId);
        public StoryListingViewModel ShareStory(int UId);
    }
}
