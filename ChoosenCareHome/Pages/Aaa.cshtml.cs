using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChoosenCareHome.Pages
{
    public class AaaModel : PageModel
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public string YearName { get; set; }
        public string MonthName { get; set; }

        public void OnGet(int? year, int? month, DateTime? date)
        {
            if (date.HasValue)
            {
                Year = date.Value.Year;
                Month = date.Value.Month;
            }
            else
            {
                Year = year ?? DateTime.Now.Year;
                Month = month ?? DateTime.Now.Month;
            }

             
        }
    }
}
