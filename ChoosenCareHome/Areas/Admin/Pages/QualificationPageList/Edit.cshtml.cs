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

namespace ChoosenCareHome.Areas.Admin.Pages.QualificationPageList
{
    public class EditModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public EditModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Qualification Qualification { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Qualifications == null)
            {
                return NotFound();
            }

            var qualification =  await _context.Qualifications.FirstOrDefaultAsync(m => m.Id == id);
            if (qualification == null)
            {
                return NotFound();
            }
            Qualification = qualification;
           ViewData["ApplicationId"] = new SelectList(_context.Applications, "Id", "Id");
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

            _context.Attach(Qualification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(Qualification.Id))
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

        private bool QualificationExists(int id)
        {
          return (_context.Qualifications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
