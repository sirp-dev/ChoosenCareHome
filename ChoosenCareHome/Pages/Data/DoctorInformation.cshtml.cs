using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages.Data
{
    public class DoctorInformationModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DoctorInformationModel(ChoosenCareHome.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var xapp = await _context.Applications.FindAsync(Application.Id);
            if (xapp != null)
            {

                xapp.DoctorName = Application.DoctorName;
                xapp.DoctorAddress = Application.DoctorAddress;
                xapp.DoctorPostcode = Application.DoctorPostcode;
                xapp.DoctorPhone = Application.DoctorPhone; 
                _context.Attach(xapp).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(Application.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./FormContinuation", new {id = xapp.Id});
        }

        private bool ApplicationExists(int id)
        {
            return (_context.Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
