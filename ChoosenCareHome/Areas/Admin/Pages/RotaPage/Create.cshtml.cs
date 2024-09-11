using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Admin.Pages.RotaPage
{
    public class CreateModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public CreateModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserRota UserRota { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
         
            var check = await _context.UserRotas.FirstOrDefaultAsync(x=>x.PostCode == UserRota.PostCode);
            if(check == null) {
                UserRota.Date = DateTime.UtcNow;
            _context.UserRotas.Add(UserRota);
            await _context.SaveChangesAsync();
            }
            else
            {
                TempData["error"] = "Already exist";
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
