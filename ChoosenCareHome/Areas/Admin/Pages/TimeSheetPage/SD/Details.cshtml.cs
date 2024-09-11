using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage.SD
{
    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public UserTimeSheet UserTimeSheet { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserTimeSheets == null)
            {
                return NotFound();
            }

            var usertimesheet = await _context.UserTimeSheets.FirstOrDefaultAsync(m => m.Id == id);
            if (usertimesheet == null)
            {
                return NotFound();
            }
            else 
            {
                UserTimeSheet = usertimesheet;
            }
            return Page();
        }
    }
}
