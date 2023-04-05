//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using CIPLATFORM.Entities.Models;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CIPLATFORM.Entities.ViewModels
//{
//  public  class ProfileViewModel
//    {

//        public long UserId { get; set; }

//        public string? FirstName { get; set; }

//        public string? LastName { get; set; }



//        public string Password { get; set; } = null!;

//        public long? PhoneNumber { get; set; }

//        public string? Avatar { get; set; }

//        public string? WhyIVolunteer { get; set; }

//        public string? EmployeeId { get; set; }

//        public string? Department { get; set; }

//        public long? CityId { get; set; }

//        public long? CountryId { get; set; }

//        public string? ProfileText { get; set; }

//        public string? LinkedInUrl { get; set; }

//        public string? Title { get; set; }

//        public int Status { get; set; }


//        public DateTime? UpdatedAt { get; set; }


//        public ResetPassword resetPass { get; set; }

//    }


//    public class ResetPassword
//    {
//        [Required(ErrorMessage = "OldPassword is Required")]
//        public string OldPassword { get; set; } = null!;

//        [Required(ErrorMessage = "Password is Required")]
//        public string Password { get; set; } = null!;

//        [Compare("Password", ErrorMessage = "Password must match")]
//        [Required(ErrorMessage = "Confirm PassWord is Required")]
//        public string ConfirmPassword { get; set; } = null!;


//    }
//}


using CIPLATFORM.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Entities.ViewModels
{
    public class ProfileViewModel
    {
        public long UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }



        public long? PhoneNumber { get; set; }

        public string? Avatar { get; set; }

        public string? WhyIVolunteer { get; set; }

        public string? EmployeeId { get; set; }

        public string? Department { get; set; }

        public long? CityId { get; set; }

        public long? CountryId { get; set; }

        public string? ProfileText { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? Title { get; set; }

        public int Status { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ResetPassword resetPass { get; set; }

    }
    public class ResetPassword
    {
        //[Required(ErrorMessage = "OldPassword is Required")]
        public string OldPassword { get; set; } = null!;
        //[Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = null!;
        //[Compare("Password", ErrorMessage = "Password must match")]
        //[Required(ErrorMessage = "Confirm PassWord is Required")]
        public string ConfirmPassword { get; set; } = null!;
    }
}