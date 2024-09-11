using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.MessagesPage
{
    public class CreateModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public CreateModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.Users.Where(x => x.Email != "info@chosenhealthcare.co.uk"), "Id", "Email");

            return Page();
        }

        [BindProperty]
        public Message Message { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if(Message.UserId == "All")
            {
                Message.All = true;
                Message.UserId = null;
            }
           
          Message.Date = DateTime.UtcNow.AddHours(1);
            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
