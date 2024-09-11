using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Admin.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public DeleteModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
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
            else
            {
                AppUser = appuser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuser = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (appuser  != null)
            {
                //delete timesheet
                var usersheet = await _context.UserTimeSheets.Where(x=>x.UserId == appuser.Id).ToListAsync();
                foreach(var i in  usersheet) {
                    var stx = await _context.UserTimeSheets.FindAsync(i.Id);

                    if (stx != null)
                    { 
                        _context.UserTimeSheets.Remove(stx);
                        
                    }
                }

                //delete timesheet
                var usermessage = await _context.Messages.Where(x => x.UserId == appuser.Id).ToListAsync();
                foreach (var i in usermessage)
                {
                    var stx = await _context.Messages.FindAsync(i.Id);

                    if (stx != null)
                    {
                        _context.Messages.Remove(stx);

                    }
                }
                await _context.SaveChangesAsync();

                await _userManager.DeleteAsync(appuser);
                TempData["success"] = "deleted successfully";
            }

            return RedirectToPage("./Index");
        }
    }
}
