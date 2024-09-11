using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Staff.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public IndexModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int TimeSheet { get; set; }
        public int Messages { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            TimeSheet = await _context.UserTimeSheets.Where(x=>x.UserId == user.Id).CountAsync();
            Messages = await _context.Messages.Where(x => x.UserId == user.Id || x.All == true & x.Read == false).CountAsync();

        }
    }
}
