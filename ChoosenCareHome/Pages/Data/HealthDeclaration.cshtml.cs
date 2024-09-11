using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages.Data
{
    public class HealthDeclarationModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public HealthDeclarationModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OccupationalHealthAssessment OccupationalHealthAssessment { get; set; } = default!;

        [BindProperty]
        public int? ApplicationId { get; set; }
        [BindProperty]
        public List<OccupationalHealthAssessment> OccupationalHealthAssessments { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var dataresult = new List<string> { "Are you in good health?",
                    "How much time have you lost from work due to illness in the last five years? Please provide details",
                    "Have you ever been treated in hospital for serious illness or surgery? Please give dates",
                    "Have you been treated in hospital during the last 12 months?",
                    "Do you have any physical disabilities that could affect your ability to carry out your assignment?",
                    "Have you ever left, been retired or denied a job on health grounds?",
                    "Have you ever been denied a driving license on health grounds?",
                    "Are you a registered disabled person?",
                    "Have you any disability related to your physical or mental health?",
                    "Have you ever suffered from any mental illness, psychological or psychiatric problems?",
                    "Do you get discomfort or pain in the chest or shortness of breath on exercise?",
                    "Have you ever had any problems with your joints, including pain, swelling or stiffness?",
                    "Do you have any difficulty in moving rapidly over short distances?",
                    "Would you have difficulty looking over either shoulder?",
                    "Do you need to wear glasses or contact lenses?",
                    "Do you have any difficulty with your eyesight which is not corrected by glasses or contact lenses?",
                    "Have you any problems working with Visual Display Units?",
                    "Have you any problems working in confined spaces/using lifts?",
                    "Do you have any difficulty hearing normal conversation?",
                    "Are you taking any medication that makes you dizzy or drowsy?",
                    "Do you have a medical condition affected by changing sleeping patterns or affecting day time sleep?",
                    "Have you suffered from any alcohol or drug related illness or had an alcohol or drug\r\nproblem?\r\n",
                    "Are you having or awaiting any treatment at the moment?",
                    "What is the date of your last chest x-ray?",
                    "Are you receiving Medicines, Pills or Tablets from a doctor or on prescription?",
                    "Have you ever suffered from any of the following?",
                    "Heart Problems/Circulatory Illness/Hypertension",
                    "High or Low Blood Pressure",
                    "Diabetes",
                    "Asthma/Hay fever",
                    "Bronchitis/Pneumonia/Pleurisy",
                    "Tuberculosis",
                    "Epilepsy/Fainting Attacks/Blackouts/Fits/Sudden Collapse",
                    "Headaches/Migraine",
                    "Psychiatric Illness/Anxiety/Depression",
                    "Dermatitis/Skin Sensitivity/Psoriasis/Eczema/Allergies",
                    "Back Injury/Back Problems/Back Pains",
                    "Recurrent Infections e.g. Sore Throats/Ear Infections/Eye Infections",
                    "Have ever had a Covid 19",
                    "Hepatitis/Jaundice",

                    };
                foreach (var item in dataresult)
                {
                    var xterm = await _context.OccupationalHealthAssessments.FirstOrDefaultAsync(x => x.Title == item && x.ApplicationId == id);
                    if (xterm == null)
                    {
                        OccupationalHealthAssessment x = new OccupationalHealthAssessment();
                        x.ApplicationId = id;
                        x.Title = item;
                        _context.OccupationalHealthAssessments.Add(x);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            OccupationalHealthAssessments = await _context.OccupationalHealthAssessments.Where(m => m.ApplicationId == id).ToListAsync();

            ApplicationId = id;
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var healthQualification in OccupationalHealthAssessments)
            {
                var data = await _context.OccupationalHealthAssessments.FindAsync(healthQualification.Id);
                data.Status = healthQualification.Status;
                data.Details = healthQualification.Details;
                _context.Attach(data).State = EntityState.Modified;

            }
            await _context.SaveChangesAsync();
            TempData["success"] = "Updated all record successfully";
            return RedirectToPage("./HealthDeclaration", new { id = ApplicationId });
        }

        private bool OccupationalHealthAssessmentExists(int id)
        {
            return (_context.OccupationalHealthAssessments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }


}
