using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace ChoosenCareHome.Areas.Admin.Pages.Users
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class MakeAdminModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly RoleManager<AppRole> _role;
        private readonly UserManager<Profile> _userManager;
        public MakeAdminModel(ChoosenCareHome.Data.ApplicationDbContext context, RoleManager<AppRole> role, UserManager<Profile> userManager)
        {
            _context = context;
            _role = role;
            _userManager = userManager;
        }

        [BindProperty]
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
            AppUser = appuser;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if(user.Role == "Admin")
                {
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
                    user.Role = "CareGiver";
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    user.Role = "Admin";
                    await _userManager.UpdateAsync(user);
                }
            }
            TempData["success"] = "Successful";
            return RedirectToPage("./Index");
        }


    }
}
