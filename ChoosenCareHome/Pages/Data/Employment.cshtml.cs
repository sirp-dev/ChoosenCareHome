using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages.Data
{
         public class EmploymentModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public EmploymentModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EmploymentHistory EmploymentHistory { get; set; } = default!;
        public List<EmploymentHistory> EmploymentHistoryList { get; set; } = default!;

        public int? ApplicationId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmploymentHistoryList = await _context.EmploymentHistories.Where(m => m.ApplicationId == id).ToListAsync();
            ApplicationId = id;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //_context.Attach(Qualification).State = EntityState.Modified;
            _context.EmploymentHistories.Add(EmploymentHistory);

            try
            {
                TempData["success"] = "Successful";
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(EmploymentHistory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            EmploymentHistoryList = await _context.EmploymentHistories.Where(m => m.ApplicationId == EmploymentHistory.ApplicationId).ToListAsync();

            return RedirectToPage("./Employment", new { id = EmploymentHistory.ApplicationId });

        }
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employmentHistory = await _context.EmploymentHistories.FindAsync(id);

            if (employmentHistory != null)
            {
                EmploymentHistory = employmentHistory;
                _context.EmploymentHistories.Remove(EmploymentHistory);
                TempData["success"] = "Successful";
                await _context.SaveChangesAsync();
            }
            EmploymentHistoryList = await _context.EmploymentHistories.Where(m => m.ApplicationId == EmploymentHistory.Id).ToListAsync();

            return RedirectToPage("./Employment", new { id = EmploymentHistory.ApplicationId });
        }
        private bool QualificationExists(int id)
        {
            return (_context.Qualifications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
