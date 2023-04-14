using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.ViewModels;
using CIPLATFORM.Respository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Respository.Repositories
{
    public class AdminRepository : IAdminRepository
    {

        public readonly CiPlatformContext _CiPlatformContext;

        public AdminRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }
        public AdminViewModel getData()
        {
            AdminViewModel um = new AdminViewModel();
            um.users = _CiPlatformContext.Users.ToList();
            um.missions= _CiPlatformContext.Missions.ToList();  

            return um;
        }
        


    }
}
