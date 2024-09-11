using ChoosenCareHome.Data.Model;
using System.ComponentModel.DataAnnotations;
using static ChoosenCareHome.Data.Model.Enum;

namespace ChoosenCareHome.Data
{
    public class UserTimeSheet
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public Profile User { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Sheet Start Time")]
        public TimeSpan StartTime { get; set; } // Represents the start time of work
        [Display(Name = "Sheet End Time")]
        public TimeSpan EndTime { get; set; }   // Represents the end time of work
        public string? Report { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }


        [Display(Name = "Rate Per Hour")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal RatePerHour { get; set; }
        public int Break { get; set; }

        public int? TimeSheetId { get; set; }
        public TimeSheet TimeSheet { get; set; }

        public bool Paid { get; set; }
        public bool GeneratedInvoice {  get; set; }
        public string? InvoiceNumber { get; set; }


        public TimesheetAcceptance TimesheetAcceptance { get; set; }
        public string? AcceptedReason { get; set; }
        public DateTime AcceptanceExpirationTime { get; set; }

        [Display(Name = "My Sheet Start Time")]
        public DateTime? UserSheetStartTime { get; set; }
        [Display(Name = "My Sheet End Time")]
        public DateTime? UserSheetEndTime { get; set; }
    }
}
