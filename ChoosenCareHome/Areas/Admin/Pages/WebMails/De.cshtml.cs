using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.WebMails
{
    public class DeModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DeModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MailSystem MailSystem { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
           
            var xMessage = await _context.MailSystems.OrderByDescending(x => x.Id).Skip(50).Take(150).ToListAsync();

            foreach (var x in xMessage)
            {
                var cvMessage = await _context.MailSystems.FirstOrDefaultAsync(m => m.Id == x.Id);
                _context.MailSystems.Remove(cvMessage);
            }
                await _context.SaveChangesAsync();

           
            return RedirectToPage("./Index");

        }

    }
}
