using System.ComponentModel;

namespace ChoosenCareHome.Data.Model
{
    public class Enum
    {
        public enum UserStatus
        {
            [Description("Pending")]
            Pending = 0,

            [Description("Active")]
            Active = 2,
            [Description("Suspended")]
            Suspended = 3,

            [Description("Leave")]
            Leave = 4,
            [Description("Deleted")]
            Deleted = 6,
        }

        public enum InvoiceStatus
        {
            Pending = 0,
            Paid = 1,
            Canceled = 2,
        }
        public enum TimesheetAcceptance
        {
            Pending = 0,
            Accepted = 1,
            Canceled = 2,
            Declined = 3,
        }
        public enum NotificationStatus
        {
            [Description("NotDefind")]
            NotDefind = 0,
            [Description("Sent")]
            Sent = 1,

            [Description("NotSent")]
            NotSent = 2,


        }
        public enum NotificationType
        {
            [Description("NotDefind")]
            NotDefind = 0,
            [Description("SMS")]
            SMS = 1,

            [Description("Email")]
            Email = 2


        }
    }
}
