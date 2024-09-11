namespace ChoosenCareHome.Data.Model
{
    public class TimeSheet
    {
        public int Id { get; set; } 

        public DateTime Date { get; set; } 

        public ICollection<UserTimeSheet> UserTimeSheet { get; set; }
    }
}
