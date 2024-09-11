using Microsoft.AspNetCore.Identity;
using static ChoosenCareHome.Data.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace ChoosenCareHome.Data.Model
{
    public class Profile : IdentityUser<string>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Surname")]
        public string? Surname { get; set; }

        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }


        public string? Role { get; set; }
        public UserStatus UserStatus { get; set; }
         
        public string? DOB { get; set; }
        public string? Address { get; set; }

        public bool ChangePass { get; set; }

        public DateTime Date { get; set; }

        public int? ApplicationId { get; set; }
    }
}
