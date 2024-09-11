using Humanizer;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography.Xml;

namespace ChoosenCareHome.Data.Model
{
    public class Application
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string? Title { get; set; }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Surname")]
        public string? Surname { get; set; }
        public string? IdNumber { get; set; }

        [Display(Name = "Date Of Birth")]
        public string? DateOfBirth { get; set; }

        [Display(Name = "National Ins. No.")]
        public string? NationalInsNo { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Display(Name = "Postcode")]
        public string? Postcode { get; set; }
        [Display(Name = "Home Tel")]
        public string? HomeTel { get; set; }
        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }
        [Display(Name = "E-Mail")]
        public string? EMail { get; set; }
        public string? UserId { get; set; }
        public Profile User { get; set; }
        [Display(Name = "Marital Status")]
        public string? MaritalStatus { get; set; }

        [Display(Name = "Next Of Kin")]
        public string? NextOfKin { get; set; }
        [Display(Name = "Relationship")]
        public string? Relationship { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Do You Have Permission To Work In The Uk")]
        public string? DoYouHavePermissionToWorkInTheUk { get; set; }
        [Display(Name = "Do You Have A Valid Passport")]
        public string? DoYouHaveAValidPassport { get; set; }
        [Display(Name = "You Have A Valid Work Permit")]
        public string? YouHaveAValidWorkPermit { get; set; }
        [Display(Name = "Do You Have Access To A Car Which Can Be Used For Work Purposes")]
        public string? DoYouHaveAccessToACarWhichCanBeUsedForWorkPurposes { get; set; }
        [Display(Name = "Do You Hold A Full Uk Driving Licence")]
        public string? DoYouHoldAFullUkDrivingLicence { get; set; }
        public ICollection<Qualification> Qualifications { get; set; }
        public ICollection<EmploymentHistory> EmploymentHistories { get; set; }
        public ICollection<ApplicationReference> ApplicationReferences { get; set; }
        public ICollection<OccupationalHealthAssessment> OccupationalHealthAssessments { get; set; }
        public ICollection<Documents> Documents { get; set; }
        public ICollection<Vacination> Vacination { get; set; }
        public ICollection<HealthQualification> HealthQualifications { get; set; }
        [Display(Name = "Doctor Name")]
        public string? DoctorName { get; set; }
        [Display(Name = "Doctor Address")]
        public string? DoctorAddress { get; set; }
        [Display(Name = "Doctor Postcode")]
        public string? DoctorPostcode { get; set; }
        [Display(Name = "Doctor Phone Number")]
        public string? DoctorPhone { get; set; }

        [Display(Name = "Full Time / Part Time")]
        public string? FullTime_PartTime { get; set; }
        [Display(Name = "If Part Time, How Many Hours Per Week Do You Want To Work")]
        public string? IfPartTime_HowManyHoursPerWeekDoYouWantToWork_HomeCareAndPopInVisits { get; set; }
        [Display(Name = "Home Care And Pop-In Visits")]
        public string? HomeCareAndPopInVisits { get; set; }
        [Display(Name = "Hospitals")]
        public string? Hospitals { get; set; }
        [Display(Name = "Nursing/Residential Homes")]
        public string? Nursing_ResidentialHomes { get; set; }
        [Display(Name = "Morning / Day / Evening / Night Sleeper Duty")]
        public string? Morning_Day_Evening_NightSleeperDuty { get; set; }


        [Display(Name = "Please State If You Are Able To Work As A 24-Hour Residential (Live-In) Carer  ")]
        public string? PleaseStateIfYouAreAbleToWorkAsA24_HourResidential_Live_In_Care { get; set; }
        [Display(Name = "If Yes, Would You Like:Long…… Or Short ……. Assignments?")]
        public string? IfYes_WouldYouLike_LongOrShort_Assignments { get; set; }
        [Display(Name = "Would You Accept A Live-In Assignment Some Distance From Your Home")]
        public string? WouldYouAcceptALive_InAssignmentSomeDistanceFromYourHome { get; set; }
        [Display(Name = "If No, Please Specify Preferred Areas")]
        public string? IfNo_PleaseSpecifyPreferredAreas { get; set; }

        [Display(Name = "Bath/Shower/Strip Wash")]
        public string? Bath_Shower_StripWash { get; set; }
        [Display(Name = "Pressure Area Care")]
        public string? PressureAreaCare { get; set; }
        [Display(Name = "Bed Bath")]
        public string? BedBath { get; set; }
        [Display(Name = "Simple Dressing Procedure")]
        public string? SimpleDressingProcedure { get; set; }
        [Display(Name = "Use Of Bath Aids")]
        public string? UseOfBathAids { get; set; }
        [Display(Name = "Assisting With Medication")]
        public string? AssistingWithMedication { get; set; }
        [Display(Name = "Shaving")]
        public string? Shaving { get; set; }
        [Display(Name = "Terminal Care")]
        public string? TerminalCare { get; set; }
        [Display(Name = "Mouth Care(Inc. Dentures")]
        public string? MouthCareIncDentures { get; set; }
        [Display(Name = "Care Of Hair")]
        public string? CareOfHair { get; set; }
        [Display(Name = "Care Of Feet(Exc.Toe Nails)")]
        public string? CareOfFeetExcToeNails { get; set; }
        [Display(Name = "Light Housework")]
        public string? LightHousework { get; set; }

        [Display(Name = "Care Of Finger Nails")]
        public string? CareOfFingerNails { get; set; }
        [Display(Name = "Washing Personal Laundry")]
        public string? WashingPersonalLaundry { get; set; }
        [Display(Name = "Dressing/Undressing")]
        public string? Dressing_Undressing { get; set; }
        [Display(Name = "Shopping")]
        public string? Shopping { get; set; }

        [Display(Name = "Bed Making/Changing Bed Linen")]
        public string? BedMaking_ChangingBedLinen { get; set; }
        [Display(Name = "Collecting Benefits")]
        public string? CollectingBenefits { get; set; }
        [Display(Name = "Continence Care")]
        public string? ContinenceCare { get; set; }
        [Display(Name = "Bedpans/Commodes Etc.")]
        public string? Bedpans_CommodesEtc { get; set; }

        [Display(Name = "Changing A Catheter Bag")]
        public string? ChangingACatheterBag { get; set; }
        [Display(Name = "Confidentiality")]
        public string? Confidentiality { get; set; }
        [Display(Name = "Emptying Catheter Bag")]
        public string? EmptyingCatheterBag { get; set; }
        [Display(Name = "Report Writing")]
        public string? ReportWriting { get; set; }
        [Display(Name = "Recording Instructions From Gp/District Nurse")]
        public string? RecordingInstructionsFromGp_DistrictNurse { get; set; }
        [Display(Name = "Observing/Recording")]
        public string? Observing_Recording { get; set; }
        [Display(Name = "Maneuvering And Handling Course")]
        public string? ManeuveringAndHandlingCourse { get; set; }
        [Display(Name = "Changes In Client’s Condition")]
        public string? ChangesInClientsCondition { get; set; }
        [Display(Name = "Use Of Hoists(Man./Elec)")]
        public string? UseOfHoistsMan_Elec { get; set; }
        [Display(Name = "Use Of Walking Aids")]
        public string? UseOfWalkingAids { get; set; }
        [Display(Name = "Private House")]
        public string? PrivateHouse { get; set; }

        [Display(Name = "Nursing/Residential")]
        public string? Nursing_Residential { get; set; }
        [Display(Name = "Home")]
        public string? Home { get; set; }

        [Display(Name = "AgeGroup")]
        public string? AgeGroup { get; set; }
        [Display(Name = "Registered Disability")]
        public string? Registereddisability { get; set; }
        [Display(Name = "Unregistered Disability")]
        public string? UnregisteredDisability { get; set; }
        [Display(Name = "No disability")]
        public string? Nodisability { get; set; }


        [Display(Name = "White European")]
        public string? EthnicOriginWhiteEuropean { get; set; }

        [Display(Name = "White Other")]
        public string? EthnicOriginWhiteOther { get; set; }
        [Display(Name = "Black African")]
        public string? EthnicOriginBlackAfrican { get; set; }
        [Display(Name = "Black Caribbean")]
        public string? EthnicOriginBlackCaribbean { get; set; }
        [Display(Name = "Black Other")]
        public string? EthnicOriginBlackOther { get; set; }
        [Display(Name = "Indian")]
        public string? Indian { get; set; }
        [Display(Name = "Pakistani")]
        public string? EthnicOriginPakistani { get; set; }
        [Display(Name = "Chinese")]
        public string? EthnicOriginChinese { get; set; }
        [Display(Name = "Other")]
        public string? EthnicOriginOther { get; set; }

        [Display(Name = "How Did You Hear About The Post?")]
        public string? HowDidYouHearAboutThePost { get; set; }

        [Display(Name = "Are You Related Or Do You Know Any Member Of Staff At Chosen Healthcare.")]
        public string? AreYouRelatedOrDoYouKnowAnyMemberOfStaffAtChosenHealthcare { get; set; }

        [Display(Name = "Have You Ever Been Convicted Of A Criminal Oﬀence? ")]
        public string? HaveYouEverBeenConvictedOfACriminalOffence { get; set; }
        [Display(Name = "If Yes, Please Give Details Of All Convictions And Cautions, Including Spent Convictions And Cautions: (Please Use A Separate Sheet If Necessary)")]
        public string? IfYesPleaseGiveDetailsOfAllConvictionsAndCautionsIncludingSpentConvictionsAndCautions { get; set; }

    }
}
