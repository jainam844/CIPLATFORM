using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Respository.Interface
{
   public interface IProfileRepository
    {
        public ProfileViewModel getUser(int UId);
        public bool saveProfile(ProfileViewModel obj, int save, int UId);
    }
}
