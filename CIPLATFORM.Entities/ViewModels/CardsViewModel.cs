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

        public CardsViewModel(List<Mission> missions, List<City> cities, List<Country> countries, List<MissionMedium> medias)
        {
            this.countries = countries;
            this.missions = missions;
            this.media = medias;
            this.cities = cities;
        }
    }
}
