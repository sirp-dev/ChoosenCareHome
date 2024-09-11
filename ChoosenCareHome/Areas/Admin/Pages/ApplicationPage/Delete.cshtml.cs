using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.ApplicationPage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DeleteModel(ChoosenCareHome.Data.ApplicationDbContext context)
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
            else 
            {
                Application = application;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }
            var application = await _context.Applications.FindAsync(id);

            if (application != null)
            {

                //.Include(x => x.Qualifications)
                //.Include(x => x.EmploymentHistories)
                //.Include(x => x.ApplicationReferences)
                //.Include(x => x.OccupationalHealthAssessments)
                //.Include(x => x.Vacination)
                //.Include(x => x.HealthQualifications)
                //.Include(x => x.Documents)

                //delete timesheet
                var Documents = await _context.Documents.Where(x => x.ApplicationId == application.Id).ToListAsync();
                foreach (var i in Documents)
                {
                    var stx = await _context.Documents.FindAsync(i.Id);

                    if (stx != null)
                    {
                        _context.Documents.Remove(stx);

                    }
                }
                var HealthQualifications = await _context.HealthQualifications.Where(x => x.ApplicationId == application.Id).ToListAsync();
                foreach (var i in HealthQualifications)
                {
                    var stx = await _context.HealthQualifications.FindAsync(i.Id);

                    if (stx != null)
                    {
                        _context.HealthQualifications.Remove(stx);

                    }
                }
                var Vacination = await _context.Vacinations.Where(x => x.ApplicationId == application.Id).ToListAsync();
                foreach (var i in Vacination)
                {
                    var stx = await _context.Vacinations.FindAsync(i.Id);

                    if (stx != null)
                    {
                        _context.Vacinations.Remove(stx);

                    }
                }
                var Qualifications = await _context.Qualifications.Where(x => x.ApplicationId == application.Id).ToListAsync();
                foreach (var i in Qualifications)
                {
                    var stx = await _context.Qualifications.FindAsync(i.Id);

                    if (stx != null)
                    {
                        _context.Qualifications.Remove(stx);

                    }
                }
                var EmploymentHistories = await _context.EmploymentHistories.Where(x => x.ApplicationId == application.Id).ToListAsync();
                foreach (var i in EmploymentHistories)
                {
                    var stx = await _context.EmploymentHistories.FindAsync(i.Id);

                    if (stx != null)
                    {
                        _context.EmploymentHistories.Remove(stx);

                    }
                }
                //
                var ApplicationReferences = await _context.ApplicationReferences.Where(x => x.ApplicationId == application.Id).ToListAsync();
                foreach (var i in ApplicationReferences)
                {
                    var stx = await _context.ApplicationReferences.FindAsync(i.Id);

                    if (stx != null)
                    {
                        _context.ApplicationReferences.Remove(stx);

                    }
                }
                //
                var OccupationalHealthAssessments = await _context.OccupationalHealthAssessments.Where(x => x.ApplicationId == application.Id).ToListAsync();
                foreach (var i in OccupationalHealthAssessments)
                {
                    var stx = await _context.OccupationalHealthAssessments.FindAsync(i.Id);

                    if (stx != null)
                    {
                        _context.OccupationalHealthAssessments.Remove(stx);

                    }
                }







                Application = application;
                _context.Applications.Remove(Application);
                await _context.SaveChangesAsync(); 
                TempData["success"] = "successful";
            }
            
            return RedirectToPage("./Index");
        }
    }
}
