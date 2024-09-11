using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public NewsBlog NewsBlog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsblog = await _context.NewsBlogs.FirstOrDefaultAsync(m => m.Id == id);
            if (newsblog == null)
            {
                return NotFound();
            }
            else
            {
                NewsBlog = newsblog;
            }
            return Page();
        }
    }

}
