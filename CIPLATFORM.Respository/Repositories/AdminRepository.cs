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
        public List<User> Usersearch(string search, int pg)
        {
            var pageSize = 5;
            List<User> users = _CiPlatformContext.Users.ToList();
           


            if (search != null)
            {
                search = search.ToLower();
                users = users.Where(x => x.FirstName.ToLower().Contains(search)).ToList();


            }
            if (pg != 0)
            {
                users = users.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }
            return users;


        }


    }
}
