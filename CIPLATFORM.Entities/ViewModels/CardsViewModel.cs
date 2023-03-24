using CIPLATFORM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Entities.ViewModels
{
    public class CardsViewModel
    {
        public List<Mission> missions { get; set; }
        public List<City> cities { get; set; }
        public List<Country> countries { get; set; }
        public List<MissionMedium> media { get; set; }
        public List<MissionRating> rating { get; set; }
        public List<MissionTheme> missionthemes { get; set; }
        public List<MissionSkill> missionskill { get; set; }

        public List<FavoriteMission> favoriteMissions { get; set; }

    }
}
