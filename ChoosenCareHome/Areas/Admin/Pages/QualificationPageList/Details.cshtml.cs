using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.QualificationPageList
{
    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Qualification Qualification { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Qualifications == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualifications.FirstOrDefaultAsync(m => m.Id == id);
            if (qualification == null)
            {
                return NotFound();
            }
            else 
            {
                Qualification = qualification;
            }
            return Page();
        }
    }
}
