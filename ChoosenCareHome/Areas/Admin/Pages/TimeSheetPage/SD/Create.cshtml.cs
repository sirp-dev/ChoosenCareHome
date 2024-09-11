using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChoosenCareHome.Data;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage.SD
{
    public class CreateModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public CreateModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TimeSheetId"] = new SelectList(_context.TimeSheets, "Id", "Id");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public UserTimeSheet UserTimeSheet { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UserTimeSheets == null || UserTimeSheet == null)
            {
                return Page();
            }

            _context.UserTimeSheets.Add(UserTimeSheet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
