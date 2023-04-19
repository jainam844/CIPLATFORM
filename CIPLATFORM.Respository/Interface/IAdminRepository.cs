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
        public AdminViewModel Usersearch(string search, int pg);
        public bool addcms(AdminViewModel obj, int command);
        public bool deleteactivity(int id,int page);
        public bool Approval(int id, int page, int status);
        public AdminViewModel EditForm(int id, string page);
    }
}
