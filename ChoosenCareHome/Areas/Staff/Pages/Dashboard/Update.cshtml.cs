using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Staff.Pages.Dashboard
{
    [Authorize]
    public class UpdateModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public UpdateModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserTimeSheet UserTimeSheet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserTimeSheets == null)
            {
                return NotFound();
            }

            var usertimesheet = await _context.UserTimeSheets.Include(x => x.TimeSheet)
.FirstOrDefaultAsync(m => m.Id == id);
            if (usertimesheet == null)
            {
                return NotFound();
            }
            UserTimeSheet = usertimesheet;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var usertimesheet = await _context.UserTimeSheets.FirstOrDefaultAsync(m => m.Id == UserTimeSheet.Id);
            usertimesheet.Report = UserTimeSheet.Report;
            _context.Attach(usertimesheet).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            TempData["success"] = "Successful";
             return RedirectToPage("./Details", new { id = usertimesheet.Id });
        }

        private bool UserTimeSheetExists(int id)
        {
            return (_context.UserTimeSheets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
