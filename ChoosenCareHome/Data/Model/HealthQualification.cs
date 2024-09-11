using System.ComponentModel.DataAnnotations;

namespace ChoosenCareHome.Data.Model
{
    public class HealthQualification
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        [Display(Name= "Certificates Date")]
        public string? Date { get; set; }

        public int? ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
