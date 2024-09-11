using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data.Model;
using System.Linq;
using System.Threading.Tasks;

namespace ChoosenCareHome.Areas.Admin.Pages.WebMails
{
    public class DeletecModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DeletecModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MailSystem MailSystem { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //MailSystem = await _context.MailSystems.FirstOrDefaultAsync(m => m.Id == id);

            //if (MailSystem == null)
            //{
            //    return NotFound();
            //}

            var xMessage = await _context.MailSystems.OrderBy(x => x.Id).Take(500).ToListAsync();

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
