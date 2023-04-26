using CIPLATFORM.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Entities.ViewModels
{
    public class AdminViewModel
    {

        public List<User>users { get; set; } = new List<User>();
        public User user { get; set; } = new User();
        public string? Avatar { get; set; }
        public IFormFile? Avatarfile { get; set; }
        public List<City> cities { get; set; } = new List<City>();
        public List<Country> countries { get; set; } = new List<Country>();

        public List<CmsPage> cmspages { get; set; }= new List<CmsPage>();
        public CmsPage CmsPage { get; set; } = new CmsPage();
        public List<Mission> missions { get; set; }=new List<Mission>();
        public List<MissionApplication>? missionapplications { get; set; } =new List<MissionApplication>();
        public List<MissionTheme> missionthemes { get; set; } = new List<MissionTheme> ();
        public MissionTheme missionTheme { get; set; }
      
        public List<Skill> skills { get; set; }
        public Skill skill { get; set; }
   
        public List<Story> stories { get; set; } =new List<Story>();
        public List<MissionTheme> newmissionThemes { get; set; } = new List<MissionTheme>();
        public List<Skill> newskills { get; set; } = new List<Skill>();
        public Mission mission { get; set; } = new Mission();
        public List<MissionMedium> missionMedia = new List<MissionMedium>();
        public List<IFormFile>? missionDocuments { get; set; }
        public List<IFormFile>? missionDs { get; set; }
        public string? url { get; set; }
        public List<long> editmissionSkills { get; set; } = new List<long>();




        public List<Banner> banners = new List<Banner>();
        public int BannerPage = 1;

        public Banner banner { get; set; } = new Banner();

    }
}
