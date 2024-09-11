using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class IndexModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public IndexModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TimeSheet> TimeSheet { get;set; } = default!;
        public int Year { get; set; }
        public int Month { get; set; }
        public async Task OnGetAsync(int? year, int? month, DateTime? date)
        {
            if (_context.TimeSheets != null)
            {
               
            }

            if (date.HasValue)
            {
                Year = date.Value.Year;
                Month = date.Value.Month;
            }
            else
            {
                Year = year ?? DateTime.Now.Year;
                Month = month ?? DateTime.Now.Month;
            }

            TimeSheet = await _context.TimeSheets
                   .Include(x => x.UserTimeSheet)
                   .Where(x => x.Date.Year == Year && x.Date.Month == Month)
                   .ToListAsync();
        }
    }
}
