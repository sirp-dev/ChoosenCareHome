using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.QualificationPageList
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
        ViewData["ApplicationId"] = new SelectList(_context.Applications, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Qualification Qualification { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Qualifications == null || Qualification == null)
            {
                return Page();
            }

            _context.Qualifications.Add(Qualification);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
