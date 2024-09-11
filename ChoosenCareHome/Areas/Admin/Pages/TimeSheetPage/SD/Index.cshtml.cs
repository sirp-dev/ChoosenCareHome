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
    public class IndexModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public IndexModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UserTimeSheet> UserTimeSheet { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UserTimeSheets != null)
            {
                UserTimeSheet = await _context.UserTimeSheets
                .Include(u => u.TimeSheet)
                .Include(u => u.User).ToListAsync();
            }
        }
    }
}
