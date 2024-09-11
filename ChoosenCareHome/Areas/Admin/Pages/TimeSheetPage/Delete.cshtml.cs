using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DeleteModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public TimeSheet TimeSheet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TimeSheets == null)
            {
                return NotFound();
            }

            var timesheet = await _context.TimeSheets.FirstOrDefaultAsync(m => m.Id == id);

            if (timesheet == null)
            {
                return NotFound();
            }
            else 
            {
                TimeSheet = timesheet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TimeSheets == null)
            {
                return NotFound();
            }
            var timesheet = await _context.TimeSheets.FindAsync(id);

            if (timesheet != null)
            {
                TimeSheet = timesheet;
                _context.TimeSheets.Remove(TimeSheet);
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
