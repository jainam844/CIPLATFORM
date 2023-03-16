using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPLATFORM.Entities.ViewModels;
namespace CIPLATFORM.Respository.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {

        public readonly CiPlatformContext _CiPlatformContext;

        public PlatformRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }


        public List<Country> GetCountryData()
        {
            List<Country> country = _CiPlatformContext.Countries.ToList();
            return country;
        }
        public List<City> GetCityData(int countryId)
        {
            List<City> city = _CiPlatformContext.Cities.Where(i => i.CountryId == countryId).ToList();
            return city;
        }
        public List<City> GetCitys()
        {
            List<City> cities = _CiPlatformContext.Cities.ToList();
            return cities;
        }

        public List<MissionTheme> GetMissionThemes()
        {
            List<MissionTheme> themes = _CiPlatformContext.MissionThemes.ToList();
            return themes;

        }

        //public List<Skill> GetSkills()
        //{
        //    List<Skill> skills = _CiPlatformContext.Skills.ToList();
        //    return skills;

        //}
        //public List<MissionSkill> GetMissionSkills()
        //{
        //    List<MissionSkill> skills = _CiPlatformContext.MissionSkills.ToList();
        //    return skills;

        //}
        //public List<MissionSkill> GetMissionSkills()
        //{

        //    var skills = _CiPlatformContext.MissionSkills.Include(m => m.Skill).ToList();
        //    return skills;

        //}

        public List<MissionSkill> GetSkills()
        {

            var skills = _CiPlatformContext.MissionSkills.Include(m => m.Skill).ToList();
            return skills;

        }
  
        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _CiPlatformContext.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.MissionMedia).Include(m => m.MissionRatings).Include(m => m.GoalMissions).Include(m => m.MissionSkills).ToList();
            return missionDetails;
        }


        public CardsViewModel getCards()
        {
            List<Mission> missions = _CiPlatformContext.Missions.ToList();
            List<MissionMedium> media = _CiPlatformContext.MissionMedia.Where(x => x.Default == 1).ToList();
            List<MissionSkill> missionSkills = _CiPlatformContext.MissionSkills.ToList();
            List<MissionTheme> missionThemes = _CiPlatformContext.MissionThemes.ToList();
            List<MissionRating> rating = _CiPlatformContext.MissionRatings.ToList();
            List<City> cities = _CiPlatformContext.Cities.ToList();
            List<Country> countries = _CiPlatformContext.Countries.ToList();




            CardsViewModel missionCards = new CardsViewModel();
            {

                missionCards.missions = missions;
                missionCards.missionthemes = missionThemes;
                missionCards.missionskill = missionSkills;
                missionCards.media = media;
                missionCards.rating = rating;
                missionCards.countries = countries;
                missionCards.cities = cities;
            }
            return missionCards;

        }
        public MissionListingViewModel GetCardDetail(int mid)
        {
            List<Mission> missions = GetMissionDetails();
            Mission mission = missions.FirstOrDefault(x => x.MissionId == mid);

            List<MissionMedium> photos = media(mid);
            
            List<Mission> relatedMissions = missions.Where(x => x.ThemeId == mission.ThemeId || x.CountryId == mission.CountryId || x.MissionSkills == mission.MissionSkills).ToList();
            relatedMissions.Remove(mission);
            MissionListingViewModel CardDetail = new MissionListingViewModel();
            {
                CardDetail.missions = mission;
                CardDetail.missionmedias = photos;

                CardDetail.relatedmissions = relatedMissions;
            }

            return CardDetail;
        }

        //public int GetMissionRatings(long missionID)
        // {
        //     MissionRating rating= _CiPlatformContext.MissionRatings.FirstOrDefault(a=>a.MissionId==missionID);
        //     return rating.Rating;
        // }




        //public CardsViewModel getCards()
        //{
        //    var cities = _CiPlatformContext.Cities.ToList();
        //    var countries = _CiPlatformContext.Countries.ToList();
        //    var missions = _CiPlatformContext.Missions.ToList();
        //    var media = _CiPlatformContext.MissionMedia.ToList();
        //    var rating = _CiPlatformContext.MissionRatings.ToList();


        //    var data = new CardsViewModel(missions, cities, countries, media);

        //    return data;
        //}

        public List<MissionMedium> media(int mid)
        {
            List<MissionMedium> photos = _CiPlatformContext.MissionMedia.Where(x => x.MissionId == mid).ToList();
            return photos;
        }
        public int GetMissionCount()
        {

            int missionNumber = _CiPlatformContext.Missions.Count();
            return missionNumber;

        }









        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort)
        {
            List<Mission> cards = new List<Mission>();
            var missioncards = GetMissionDetails();
            var Missionskills = GetSkills();
            List<int> temp = new List<int>();


            if (cityId.Count != 0 || countryId.Count != 0 || themeId.Count != 0 || skillId.Count != 0)


            {

                foreach (var n in countryId)
                {
                    foreach (var item in missioncards)
                    {
                        bool countrychek = cards.Any(x => x.MissionId == item.MissionId);
                        if (item.CountryId == n && countrychek == false)
                        {
                            cards.Add(item);
                        }
                    }

                }

                if (cityId.Count != 0)
                {
                    cards.Clear();
                    foreach (var n in cityId)
                    {
                        foreach (var item in missioncards)
                        {
                            bool citychek = cards.Any(x => x.MissionId == item.MissionId);
                            if (item.CityId == n && citychek == false)
                            {
                                cards.Add(item);
                            }

                        }
                    }
                }



                foreach (var n in themeId)
                {
                    foreach (var item in missioncards)
                    {
                        bool themechek = cards.Any(x => x.MissionId == item.MissionId);
                        if (item.ThemeId == n && themechek == false)
                        {
                            cards.Add(item);
                        }
                    }
                }

                foreach (var n in skillId)
                {
                    foreach (var item in Missionskills)
                    {
                        bool skillchek = cards.Any(x => x.MissionId == item.MissionId);
                        if (item.SkillId == n && skillchek == false)
                        {

                            cards.Add(missioncards.FirstOrDefault(x => x.MissionId == item.MissionId));
                        }
                    }
                    //    foreach (var item in Missionskills)
                    //    {
                    //        if (item.SkillId == n)
                    //        {
                    //            temp.Add((int)item.MissionId);
                    //        }
                    //        foreach (var item2 in temp)
                    //        {
                    //            bool skillchek = missionDetails.Any(x => x.MissionId == item2);
                    //            if (skillchek == false)
                    //            {
                    //                cards.Add(missioncards.FirstOrDefault(x => x.MissionId == item2));
                    //            }
                    //        }

                    //    }
                    //}

                }
                if (search != null)
                {

                    foreach (var n in missioncards)
                    {

                        var title = n.Title.ToLower();
                        if (title.Contains(search.ToLower()))
                        {
                            cards.Add(n);
                        }
                    }

                }

                if (sort != null)
                {


                    if (sort == 1)
                    {
                        //if (cards.Count != 0)
                        //{
                        cards = cards.OrderByDescending(x => x.CreatedAt).ToList();
                        //}

                        //else
                        //{
                        //    missioncards = missioncards.OrderByDescending(x => x.CreatedAt).ToList();
                        //    return missioncards;
                        //}
                    }
                    if (sort == 2)
                    {
                        //if (cards.Count != 0)
                        //{
                        cards = cards.OrderBy(x => x.CreatedAt).ToList();
                        //}

                        //else
                        //{
                        //    missioncards = missioncards.OrderBy(x => x.CreatedAt).ToList();
                        //    return missioncards;
                        //}
                    }




                }




                return cards;
            }

            else if (cityId.Count == 0 && countryId.Count == 0 && themeId.Count == 0 && skillId.Count == 0 && search == null)
            {
                foreach (var item in missioncards)
                {
                    cards.Add(item);
                }
                //return cards;
            }

            if (search != null)
            {
                foreach (var n in missioncards)
                {
                    var title = n.Title.ToLower();
                    if (title.Contains(search.ToLower()))
                    {
                        cards.Add(n);
                    }
                }

            }

            if (sort != null)
            {


                if (sort == 1)
                {
                    //if (cards.Count != 0)
                    //{
                    //    cards = cards.OrderByDescending(x => x.CreatedAt).ToList();
                    //}

                    //else
                    //{
                    missioncards = missioncards.OrderByDescending(x => x.CreatedAt).ToList();
                    return missioncards;
                    //}
                }
                if (sort == 2)
                {
                    //if (cards.Count != 0)
                    //{
                    //    cards = cards.OrderBy(x => x.CreatedAt).ToList();
                    //}

                    //else
                    //{
                    missioncards = missioncards.OrderBy(x => x.CreatedAt).ToList();
                    return missioncards;
                    //}
                }




            }


            return cards;

        }







    }
}
