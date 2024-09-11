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

namespace ChoosenCareHome.Areas.Admin.Pages.HealthCareQualificationPage
{
    public class EditModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public EditModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public HealthQualification HealthQualification { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.HealthQualifications == null)
            {
                return NotFound();
            }

            var healthqualification =  await _context.HealthQualifications.FirstOrDefaultAsync(m => m.Id == id);
            if (healthqualification == null)
            {
                return NotFound();
            }
            HealthQualification = healthqualification;
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

            _context.Attach(HealthQualification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthQualificationExists(HealthQualification.Id))
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

        private bool HealthQualificationExists(int id)
        {
          return (_context.HealthQualifications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
