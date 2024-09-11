using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Admin.Pages.Users
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class IndexModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public IndexModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Profile> AppUser { get; set; } 

        public async Task OnGetAsync()
        {

            AppUser = await _userManager.Users.Where(x=>x.Email != "info@chosenhealthcare.co.uk").ToListAsync();
            
        }
    }
}
