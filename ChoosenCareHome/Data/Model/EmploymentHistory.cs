using System.ComponentModel.DataAnnotations;

namespace ChoosenCareHome.Data.Model
{
    public class EmploymentHistory
    {
        public int Id { get; set; }
        [Display(Name = "Name Of Employer")]
        public string? NameOfEmployer { get; set; }
        [Display(Name = "Phone Of Employer")]
        public string? PhoneOfEmployer { get; set; }
        [Display(Name = "Address Of Employer")]
        public string? AddressOfEmployer { get; set; }
        [Display(Name = "From")]
        public string? From { get; set; }
        public string? To { get; set; }
        [Display(Name = "Position Held, Duties And Responsibilities")]
        public string? PositionDuties { get; set; }
        [Display(Name = "Reason For Leaving")]
        public string? Reason { get; set; }

        public int? ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
