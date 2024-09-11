using System.ComponentModel.DataAnnotations;

namespace ChoosenCareHome.Data.Model
{
    public class OccupationalHealthAssessment
    {
        public int Id { get; set; }
        [Display(Name = "Occupational Health Assessment")]
        public string? Title { get; set; }
        public string? Status { get; set; }
        public string? Details { get; set; }
         
        public int? ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
