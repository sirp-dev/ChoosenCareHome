using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages
{
      public class NewsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public NewsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<NewsBlog> NewsBlog { get; set; } = default!;

        public async Task OnGetAsync()
        {

            NewsBlog = await _context.NewsBlogs.ToListAsync();

        }
    }
}
