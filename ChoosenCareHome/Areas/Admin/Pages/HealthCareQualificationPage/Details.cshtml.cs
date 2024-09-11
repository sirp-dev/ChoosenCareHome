using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.HealthCareQualificationPage
{
    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public HealthQualification HealthQualification { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.HealthQualifications == null)
            {
                return NotFound();
            }

            var healthqualification = await _context.HealthQualifications.FirstOrDefaultAsync(m => m.Id == id);
            if (healthqualification == null)
            {
                return NotFound();
            }
            else 
            {
                HealthQualification = healthqualification;
            }
            return Page();
        }
    }
}
