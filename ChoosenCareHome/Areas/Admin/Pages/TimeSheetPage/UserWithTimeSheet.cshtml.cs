using ChoosenCareHome.Data.Model;
using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    public class UserWithTimeSheetModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public UserWithTimeSheetModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<UserListDto> UserList { get; set; }
        public string MonthYearTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Year { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Month { get; set; }

        public async Task<IActionResult> OnGetAsync(int year, int month)
        {
            Year = year;
            Month = month;

            var appUsers = await _userManager.Users.Where(x => x.Email != "info@chosenhealthcare.co.uk").ToListAsync();
            var userTimeSheets = await _context.UserTimeSheets
                .Include(x=>x.TimeSheet)
                .Where(x => x.TimeSheet.Date.Year == year && x.TimeSheet.Date.Month == month)
.ToListAsync();

            UserList = await GetUserListAsync(appUsers, userTimeSheets);
            UserList = UserList.Where(x => x.TotalSheets > 0).ToList();
            if(year > 0)
            {
                MonthYearTitle = new DateTime(year, month, 1).ToString("MMMM yyyy");

            }
            return Page();
        }

        public async Task<List<UserListDto>> GetUserListAsync(List<Profile> profiles, List<UserTimeSheet> userTimeSheets)
        {
            return await Task.Run(() =>
            {
                return profiles.Select(profile => new UserListDto
                {
                    FullName = $"{profile.Title} {profile.FirstName} {profile.MiddleName} {profile.Surname}".Trim(),
                    Email = profile.Email,
                    Phone = profile.PhoneNumber,
                    Role = profile.Role,
                    Status = profile.UserStatus.ToString(),
                    UserId = profile.Id,
                    TotalSheets = userTimeSheets.Count(uts => uts.UserId == profile.Id && uts.TimeSheet.Date.Year == Year && uts.TimeSheet.Date.Month == Month)

                }).ToList();
            });
        }
    }

}
