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

namespace ChoosenCareHome.Pages.Data
{
    public class HealthCareQualificationModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public HealthCareQualificationModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public HealthQualification HealthQualification { get; set; } = default!;

        [BindProperty]
        public int? ApplicationId { get; set; }
        [BindProperty]
        public List<HealthQualification> HealthQualifications { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var dataresult = new List<string> { "Manual handling",
                    "Health and safety",
                    "Basic food hygiene",
                    "First aid",
                    "NVQ levels",
                    "Others (please list)",  
                    "Others 1",  
                    "Others 2",  
                    "Others 3",  
                    "Others 4",   
                    };
                foreach (var item in dataresult)
                {
                    var xterm = await _context.HealthQualifications.FirstOrDefaultAsync(x => x.Name == item && x.ApplicationId == id);
                    if (xterm == null)
                    {
                        HealthQualification x = new HealthQualification();
                        x.ApplicationId = id;
                        x.Name = item;
                        _context.HealthQualifications.Add(x);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            HealthQualifications = await _context.HealthQualifications.Where(m => m.ApplicationId == id).ToListAsync();

            ApplicationId = id;
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var healthQualification in HealthQualifications)
            {
                var data = await _context.HealthQualifications.FindAsync(healthQualification.Id);
                data.Status = healthQualification.Status;
                data.Date = healthQualification.Date;
                _context.Attach(data).State = EntityState.Modified;
                
            }
            await _context.SaveChangesAsync();
            TempData["success"] = "Updated all record successfully";
            return RedirectToPage("./HealthCareQualification", new { id = ApplicationId });
        }

        private bool HealthQualificationExists(int id)
        {
            return (_context.HealthQualifications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
