using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class SheetDetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public SheetDetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
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

            var usertimesheet = await _context.UserTimeSheets
                .Include(x=>x.TimeSheet)
                .Include(x=>x.User)
                .FirstOrDefaultAsync(m => m.Id == id);
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
