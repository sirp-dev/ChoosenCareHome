using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace ChoosenCareHome.Areas.Admin.Pages.Users
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Profile AppUser { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuser = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (appuser == null)
            {
                return NotFound();
            }
            else 
            {
                AppUser = appuser;
            }
            return Page();
        }
    }
}
