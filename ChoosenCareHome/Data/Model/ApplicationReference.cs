using System.ComponentModel.DataAnnotations;

namespace ChoosenCareHome.Data.Model
{
    public class ApplicationReference
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [Display(Name= "Name Of Referee")]
        public string? Name { get; set; }
        [Display(Name = "Name Of Employer")]
        public string? NameEmployer { get; set; }
        [Display(Name = "Address Of Employer")]
        public string? Address { get; set; }
        [Display(Name = "Worked: From")]
        public string? From { get; set; }
        [Display(Name = "To")]
        public string? To { get; set; }
        [Display(Name = "Telephone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "E-Mail")]
        public string? Email { get; set; }
        [Display(Name = "Fax Number")]
        public string? FaxNumber { get; set; }

        public int? ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
