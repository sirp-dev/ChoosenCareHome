using static ChoosenCareHome.Data.Model.Enum;

namespace ChoosenCareHome.Data.Model
{
    public class MailSystem
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Mail { get; set; }
        public string? Title { get; set; }
        public string? Result { get; set; }
        public int Retries { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
