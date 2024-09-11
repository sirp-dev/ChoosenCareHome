using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Data
{
    public class ApplicationDbContext : IdentityDbContext<Profile, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationReference> ApplicationReferences { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public DbSet<HealthQualification> HealthQualifications { get; set; }
        public DbSet<OccupationalHealthAssessment> OccupationalHealthAssessments { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Vacination> Vacinations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<NewsBlog> NewsBlogs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<UserTimeSheet> UserTimeSheets { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<UserRota> UserRotas { get; set; }
        public DbSet<MailSystem> MailSystems { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
        }

     }
}
