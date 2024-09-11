using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage.SD
{
    public class EditModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public EditModel(ChoosenCareHome.Data.ApplicationDbContext context)
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

            var usertimesheet =  await _context.UserTimeSheets.FirstOrDefaultAsync(m => m.Id == id);
            if (usertimesheet == null)
            {
                return NotFound();
            }
            UserTimeSheet = usertimesheet;
           ViewData["TimeSheetId"] = new SelectList(_context.TimeSheets, "Id", "Id");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserTimeSheet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTimeSheetExists(UserTimeSheet.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserTimeSheetExists(int id)
        {
          return (_context.UserTimeSheets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
