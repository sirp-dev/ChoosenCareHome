using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages.Data
{
    public class SuccessFeedbackModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public SuccessFeedbackModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Application Application { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            Application = application;
            return Page();
        }

    }
}
