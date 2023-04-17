using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Respository.Interface
{
    public interface IAdminRepository
    {
        public AdminViewModel getData();
        public List<User> Usersearch(string search, int pg);

    }
}
