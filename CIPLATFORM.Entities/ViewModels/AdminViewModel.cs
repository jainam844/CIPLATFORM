using CIPLATFORM.Entities.Models;
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
        public List<CmsPage> cmspages { get; set; }= new List<CmsPage>();
        public List<Mission> missions { get; set; }=new List<Mission>();
        public List<MissionApplication>? missionapplications { get; set; } =new List<MissionApplication>();

        public List<Story> stories { get; set; } =new List<Story>();
    }
}
