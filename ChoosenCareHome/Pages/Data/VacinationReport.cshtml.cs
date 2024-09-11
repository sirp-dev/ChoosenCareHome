using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages.Data
{
        public class VacinationReportModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public VacinationReportModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vacination Vacination { get; set; } = default!;

        [BindProperty]
        public int? ApplicationId { get; set; }
        [BindProperty]
        public List<Vacination> Vacinations { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var dataresult = new List<string> { "Tuberculosis incl BCG, Heaf, Mantoux or Tine",

                    "Rubella (German Measles)",
                    "Poliomyelitis",
                    "Hepatitis B",
                    "Hepatitis B Antibodies Date and Result",
                    "HIV",
                    "Tetanus",
                    "Typhoid",
                    "Covid 19 vaccine ( First dose/ second Dose)",
                    "Other",

                    };
                foreach (var item in dataresult)
                {
                    var xterm = await _context.Vacinations.FirstOrDefaultAsync(x => x.Title == item && x.ApplicationId == id);
                    if (xterm == null)
                    {
                        Vacination x = new Vacination();
                        x.ApplicationId = id;
                        x.Title = item;
                        _context.Vacinations.Add(x);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            Vacinations = await _context.Vacinations.Where(m => m.ApplicationId == id).ToListAsync();

            ApplicationId = id;
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var healthQualification in Vacinations)
            {
                var data = await _context.Vacinations.FindAsync(healthQualification.Id);
                data.Status = healthQualification.Status;
                data.Details = healthQualification.Details;
                _context.Attach(data).State = EntityState.Modified;

            }
            await _context.SaveChangesAsync();
            TempData["success"] = "Updated all record successfully";
            return RedirectToPage("./VacinationReport", new { id = ApplicationId });
        }

        private bool VacinationExists(int id)
        {
            return (_context.Vacinations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
