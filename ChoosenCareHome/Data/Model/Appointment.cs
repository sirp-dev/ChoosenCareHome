using System.ComponentModel.DataAnnotations;

namespace ChoosenCareHome.Data.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string? Title { get; set; }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Surname")]
        public string? Surname { get; set; }

        [Display(Name = "Date Of Birth")]
        public string? DateOfBirth { get; set; }
        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Display(Name = "Postcode")]
        public string? Postcode { get; set; }
        [Display(Name = "Home Tel")]
        public string? HomeTel { get; set; }
        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }
        [Display(Name = "E-Mail")]
        public string? EMail { get; set; }

        [Display(Name = "Marital Status")]
        public string? MaritalStatus { get; set; }

        [Display(Name = "Subject")]
        public string? Subject { get; set; }


        [Display(Name = "Message")]
        public string? Message { get; set; }
    }
}
