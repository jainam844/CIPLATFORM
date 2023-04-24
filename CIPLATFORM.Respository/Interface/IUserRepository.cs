using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Respository.Interface
{
    public interface IUserRepository
    {
        //User login(User obj);
        public Login login(Login obj);
    User register(User obj);
        User forgot(User obj);
        PasswordReset newpassword(User obj, string token);
        public bool checktime(string token);
        public bool checktoken(string token);
    }
   
}
