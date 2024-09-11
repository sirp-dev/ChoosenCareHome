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
    public class IndexModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public IndexModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<HealthQualification> HealthQualification { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.HealthQualifications != null)
            {
                HealthQualification = await _context.HealthQualifications
                .Include(h => h.Application).ToListAsync();
            }
        }
    }
}
