using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChoosenCareHome.Areas.Staff.Pages.Dashboard
{
     [Authorize]
    public class TimeSheetModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public TimeSheetModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<UserTimeSheet> TimeSheet { get; set; }
        public List<UserTimeSheet> PendingTimeSheet { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public async Task OnGetAsync(int? year, int? month, DateTime? date)
        {
            var user = await _userManager.GetUserAsync(User);

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


            TimeSheet = await _context.UserTimeSheets
                .Include(x => x.TimeSheet)
                 .Where(x => x.TimeSheet.Date.Year == Year && x.TimeSheet.Date.Month == Month)
                .Where(x => x.UserId == user.Id).ToListAsync();

            PendingTimeSheet = await _context.UserTimeSheets
              .Include(x => x.TimeSheet)
               .Where(x => x.UserId == null).ToListAsync();

        }
    }
}
