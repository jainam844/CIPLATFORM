using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using CIPLATFORM.Respository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Respository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        public readonly CiPlatformContext _CiPlatformContext;

        public ProfileRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }


        public ProfileViewModel getUser(int UId)
        {

            User user = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == UId);
            ProfileViewModel pm = new ProfileViewModel();
            if (user != null)
            {
                {
                    pm.UserId = user.UserId;
                    pm.FirstName = user.FirstName;
                    pm.LastName = user.LastName;
                    pm.EmployeeId = user.EmployeeId;
                    pm.Title = user.Title;
                    pm.Department = user.Department;
                    pm.ProfileText = user.ProfileText;
                    pm.Department = user.Department;
                    pm.WhyIVolunteer = user.WhyIVolunteer;
                    pm.CountryId = user.CountryId;
                    pm.CityId = user.CityId;
                    pm.LinkedInUrl = user.LinkedInUrl;
                }

            }
            return pm;
        }
        public bool saveProfile(ProfileViewModel obj,int save,int UId)
        {
            User user=_CiPlatformContext.Users.FirstOrDefault(x=>x.UserId == UId);
          

            if (user != null)
            {
                if (save == 1)
                {
                    {
                        user.Password = obj.resetPass.Password;
                    }
                    _CiPlatformContext.Users.Update(user);
                    _CiPlatformContext.SaveChanges();
                }
            }
            return true ;
        }


    }
}
