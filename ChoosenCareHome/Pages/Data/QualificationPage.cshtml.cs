using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Pages.Data
{
    public class QualificationPageModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public QualificationPageModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Qualification Qualification { get; set; } = default!;
        public List<Qualification> QualificationList { get; set; } = default!;

        public int? ApplicationId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QualificationList =  await _context.Qualifications.Where(m => m.ApplicationId == id).ToListAsync();
            ApplicationId = id;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //_context.Attach(Qualification).State = EntityState.Modified;
            _context.Qualifications.Add(Qualification);

            try
            {
                TempData["success"] = "Successful";
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(Qualification.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            QualificationList = await _context.Qualifications.Where(m => m.ApplicationId == Qualification.ApplicationId).ToListAsync();

            return RedirectToPage("./QualificationPage", new { id = Qualification.ApplicationId });

        }
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            var qualification = await _context.Qualifications.FindAsync(id);

            if (qualification != null)
            {
                Qualification = qualification;
                _context.Qualifications.Remove(Qualification);
                TempData["success"] = "Successful";
                await _context.SaveChangesAsync();
            }
            QualificationList = await _context.Qualifications.Where(m => m.ApplicationId == Qualification.Id).ToListAsync();

            return RedirectToPage("./QualificationPage", new { id = Qualification.ApplicationId });
        }
        private bool QualificationExists(int id)
        {
          return (_context.Qualifications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
