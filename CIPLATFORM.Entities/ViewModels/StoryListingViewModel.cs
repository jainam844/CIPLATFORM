using CIPLATFORM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Entities.ViewModels
{
    public class StoryListingViewModel
    {
        public Story story { get; set; }
        public List<Story> stories { get; set; }
        public List<StoryMedium> storymedias { get; set; }
        public List<User>? coworkers { get; set; }

        public List<MissionApplication> missions { get; set; }

        public List<string> simg { get; set; } = new List<string>();
        public string url { get; set; }

    }
}
