using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.RotaPage
{
    public class DeleteModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DeleteModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UserRota UserRota { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserRotas == null)
            {
                return NotFound();
            }

            var userclient = await _context.UserRotas.FirstOrDefaultAsync(m => m.Id == id);

            if (userclient == null)
            {
                return NotFound();
            }
            else 
            {
                UserRota = userclient;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserRotas == null)
            {
                return NotFound();
            }
            var userclient = await _context.UserRotas.FindAsync(id);

            if (userclient != null)
            {
                UserRota = userclient;
                _context.UserRotas.Remove(UserRota);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
