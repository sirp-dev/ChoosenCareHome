using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages.Data
{
        public class FormContinuationModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public FormContinuationModel(ChoosenCareHome.Data.ApplicationDbContext context)
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

                xapp.FullTime_PartTime = Application.FullTime_PartTime;
                xapp.IfPartTime_HowManyHoursPerWeekDoYouWantToWork_HomeCareAndPopInVisits = Application.IfPartTime_HowManyHoursPerWeekDoYouWantToWork_HomeCareAndPopInVisits;
                xapp.HomeCareAndPopInVisits = Application.HomeCareAndPopInVisits;
                xapp.Hospitals = Application.Hospitals;
                xapp.Nursing_ResidentialHomes = Application.Nursing_ResidentialHomes;
                xapp.Morning_Day_Evening_NightSleeperDuty = Application.Morning_Day_Evening_NightSleeperDuty;
                xapp.PleaseStateIfYouAreAbleToWorkAsA24_HourResidential_Live_In_Care = Application.PleaseStateIfYouAreAbleToWorkAsA24_HourResidential_Live_In_Care;
                xapp.IfYes_WouldYouLike_LongOrShort_Assignments = Application.IfYes_WouldYouLike_LongOrShort_Assignments;
                xapp.WouldYouAcceptALive_InAssignmentSomeDistanceFromYourHome = Application.WouldYouAcceptALive_InAssignmentSomeDistanceFromYourHome;
                xapp.IfNo_PleaseSpecifyPreferredAreas = Application.IfNo_PleaseSpecifyPreferredAreas;
                xapp.Bath_Shower_StripWash = Application.Bath_Shower_StripWash;
                xapp.BedBath = Application.BedBath;
                xapp.UseOfBathAids = Application.UseOfBathAids;
                xapp.Shaving = Application.Shaving;
                xapp.MouthCareIncDentures = Application.MouthCareIncDentures;
                xapp.CareOfHair = Application.CareOfHair;
                xapp.Dressing_Undressing = Application.Dressing_Undressing;
                xapp.LightHousework = Application.LightHousework;
                xapp.WashingPersonalLaundry = Application.WashingPersonalLaundry;
                xapp.Shopping = Application.Shopping;
                xapp.BedMaking_ChangingBedLinen = Application.BedMaking_ChangingBedLinen;
                xapp.CollectingBenefits = Application.CollectingBenefits;
                xapp.ContinenceCare = Application.ContinenceCare;
                xapp.Bedpans_CommodesEtc = Application.Bedpans_CommodesEtc;
                xapp.ChangingACatheterBag = Application.ChangingACatheterBag;
                xapp.EmptyingCatheterBag = Application.EmptyingCatheterBag;
                xapp.Confidentiality = Application.Confidentiality;
                xapp.ReportWriting = Application.ReportWriting;
                xapp.RecordingInstructionsFromGp_DistrictNurse = Application.RecordingInstructionsFromGp_DistrictNurse;
                xapp.Observing_Recording = Application.Observing_Recording;
                xapp.ChangesInClientsCondition = Application.ChangesInClientsCondition;
                xapp.ManeuveringAndHandlingCourse = Application.ManeuveringAndHandlingCourse;
                xapp.UseOfHoistsMan_Elec = Application.UseOfHoistsMan_Elec;
                xapp.UseOfWalkingAids = Application.UseOfWalkingAids;
                xapp.PrivateHouse = Application.PrivateHouse;
                xapp.Nursing_Residential = Application.Nursing_Residential;
                xapp.Home = Application.Home;
                xapp.AgeGroup = Application.AgeGroup;
                xapp.Registereddisability = Application.Registereddisability;
                xapp.UnregisteredDisability = Application.UnregisteredDisability;
                xapp.Nodisability = Application.Nodisability;
                xapp.EthnicOriginWhiteEuropean = Application.EthnicOriginWhiteEuropean;
                xapp.EthnicOriginWhiteOther = Application.EthnicOriginWhiteOther;
                xapp.EthnicOriginBlackAfrican = Application.EthnicOriginBlackAfrican;
                xapp.EthnicOriginBlackCaribbean = Application.EthnicOriginBlackCaribbean;
                xapp.EthnicOriginBlackOther = Application.EthnicOriginBlackOther;
                xapp.Indian = Application.Indian;
                xapp.EthnicOriginPakistani = Application.EthnicOriginPakistani;
                xapp.EthnicOriginChinese = Application.EthnicOriginChinese;
                xapp.EthnicOriginOther = Application.EthnicOriginOther;
                xapp.HowDidYouHearAboutThePost = Application.HowDidYouHearAboutThePost;
                xapp.AreYouRelatedOrDoYouKnowAnyMemberOfStaffAtChosenHealthcare = Application.AreYouRelatedOrDoYouKnowAnyMemberOfStaffAtChosenHealthcare;
                xapp.HaveYouEverBeenConvictedOfACriminalOffence = Application.HaveYouEverBeenConvictedOfACriminalOffence;
                xapp.IfYesPleaseGiveDetailsOfAllConvictionsAndCautionsIncludingSpentConvictionsAndCautions = Application.IfYesPleaseGiveDetailsOfAllConvictionsAndCautionsIncludingSpentConvictionsAndCautions;
                
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

            return RedirectToPage("./DocumantUpload", new { id = xapp.Id });
        }

        private bool ApplicationExists(int id)
        {
            return (_context.Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
