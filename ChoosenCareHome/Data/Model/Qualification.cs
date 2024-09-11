using System.ComponentModel.DataAnnotations;

namespace ChoosenCareHome.Data.Model
{
    public class Qualification
    {
        public int Id { get; set; }
        [Display(Name = "Qualifications")]
        public string? QualificationsItem { get; set; }
        [Display(Name = "School/College")]
        public string? SchoolCollege { get; set; }
        [Display(Name = "Grade/Result")]
        public string? GradeResult { get; set; }
        [Display(Name = "Dates: From-To")]
        public string? DatesFromTo { get; set; }
        public int? ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
