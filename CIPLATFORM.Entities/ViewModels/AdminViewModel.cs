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
        public List<Mission> missions { get; set; }=new List<Mission>();
    }
}
