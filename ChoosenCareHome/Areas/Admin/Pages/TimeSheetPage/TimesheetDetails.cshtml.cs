using ChoosenCareHome.Data.Model;
using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    public class TimesheetDetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public TimesheetDetailsModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<UserTimeSheet> UserTimeSheets { get; set; }
        public Profile UserProfile { get; set; }
        public List<UserListDto> UserList { get; set; }
        public string MonthYearTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Year { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Month { get; set; }
        public async Task<IActionResult> OnGetAsync(string id, int year, int month)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            Year = year;
            Month = month;
            UserProfile = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            MonthYearTitle = new DateTime(year, month, 1).ToString("MMMM yyyy");
            if (UserProfile == null)
            {
                return NotFound();
            }

            UserTimeSheets = await _context.UserTimeSheets
                .Include(x=>x.TimeSheet)
                .Where(uts => uts.UserId == id && uts.TimeSheet.Date.Year == year && uts.TimeSheet.Date.Month == month)
                .ToListAsync();

            return Page();
        }
    }

}
