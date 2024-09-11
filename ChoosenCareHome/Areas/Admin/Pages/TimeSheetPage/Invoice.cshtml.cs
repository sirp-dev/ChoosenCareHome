using ChoosenCareHome.Data.Model;
using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    public class InvoiceModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public InvoiceModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Invoice Invoice { get; set; }
       
        public string Fullname { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {


            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            Invoice = await _context.Invoices
                .Include(x=>x.User)
               .FirstOrDefaultAsync(uts => uts.Id == id);

          
            Fullname = Invoice.User.FirstName +" " + Invoice.User.Surname;
           

            return Page();
        }
    }
}

