using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.NewsBlogPage
{
    public class DeleteModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DeleteModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public NewsBlog NewsBlog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.NewsBlogs == null)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.NewsBlogs == null)
            {
                return NotFound();
            }
            var newsblog = await _context.NewsBlogs.FindAsync(id);

            if (newsblog != null)
            {
                NewsBlog = newsblog;
                _context.NewsBlogs.Remove(NewsBlog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
