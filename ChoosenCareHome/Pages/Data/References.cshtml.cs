using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages.Data
{
    public class ReferencesModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public ReferencesModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApplicationReference ApplicationReference { get; set; } = default!;
        public List<ApplicationReference> ApplicationReferenceList { get; set; } = default!;

        public int? ApplicationId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationReferenceList = await _context.ApplicationReferences.Where(m => m.ApplicationId == id).ToListAsync();
            ApplicationId = id;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //_context.Attach(Qualification).State = EntityState.Modified;
            _context.ApplicationReferences.Add(ApplicationReference);

            try
            {
                TempData["success"] = "Successful";
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(ApplicationReference.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ApplicationReferenceList = await _context.ApplicationReferences.Where(m => m.ApplicationId == ApplicationReference.ApplicationId).ToListAsync();

            return RedirectToPage("./References", new { id = ApplicationReference.ApplicationId });

        }
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employmentHistory = await _context.ApplicationReferences.FindAsync(id);

            if (employmentHistory != null)
            {
                ApplicationReference = employmentHistory;
                _context.ApplicationReferences.Remove(ApplicationReference);
                TempData["success"] = "Successful";
                await _context.SaveChangesAsync();
            }
            ApplicationReferenceList = await _context.ApplicationReferences.Where(m => m.ApplicationId == ApplicationReference.Id).ToListAsync();

            return RedirectToPage("./References", new { id = ApplicationReference.ApplicationId });
        }
        private bool QualificationExists(int id)
        {
            return (_context.Qualifications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
