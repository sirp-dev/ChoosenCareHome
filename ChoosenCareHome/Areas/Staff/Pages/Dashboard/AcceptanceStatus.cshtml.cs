using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static ChoosenCareHome.Data.Model.Enum;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

namespace ChoosenCareHome.Areas.Staff.Pages.Dashboard
{
    [Authorize]
    public class AcceptanceStatusModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly UserManager<Profile> _userManager;

        public AcceptanceStatusModel(ChoosenCareHome.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnv, UserManager<Profile> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        [BindProperty]
        public UserTimeSheet UserTimeSheet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserTimeSheets == null)
            {
                return NotFound();
            }

            var usertimesheet = await _context.UserTimeSheets.Include(x => x.TimeSheet)
.FirstOrDefaultAsync(m => m.Id == id);
            if (usertimesheet == null)
            {
                return NotFound();
            }
            UserTimeSheet = usertimesheet;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var usertimesheet = await _context.UserTimeSheets
                .Include(x => x.User)
                .Include(x => x.TimeSheet)
                .FirstOrDefaultAsync(m => m.Id == UserTimeSheet.Id);
            usertimesheet.TimesheetAcceptance = UserTimeSheet.TimesheetAcceptance;
            usertimesheet.AcceptedReason = UserTimeSheet.AcceptedReason;

            //
            if(usertimesheet.UserId == null)
            {
                usertimesheet.UserId = user.Id;
            }

            _context.Attach(usertimesheet).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            TempData["success"] = "Successful";


            //send email
            string title = "TimeSheet Acceptance Update";
            string msg = $"{usertimesheet.User.Surname + usertimesheet.User.FirstName} has {usertimesheet.TimesheetAcceptance} the sheet for {usertimesheet.TimeSheet.Date.Date} at {usertimesheet.PostCode} <br> Time: {usertimesheet.StartTime} - {usertimesheet.EndTime}";

            try
            {
                //email
                StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "Mail.html"));
                //create the mail message 
                MailMessage mail = new MailMessage();

                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{title}", title);
                mailmsg = mailmsg.Replace("{MESSAGE}", msg);
                mailmsg = mailmsg.Replace("{name}", "Admin");
                mail.Body = mailmsg;
                sr.Close();
                MailSystem ms = new MailSystem();
                ms.Email = "info@chosenhealthcare.co.uk";
                ms.Title = title;
                ms.Mail = mailmsg;
                ms.Retries = 0; ms.NotificationType = NotificationType.Email;
                ms.NotificationStatus = NotificationStatus.NotSent;
                _context.MailSystems.Add(ms);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

            }

            try
            {
                //sms

                MailSystem ms = new MailSystem();
                ms.Email = "+447852528020";
                ms.Title = title;
                ms.Mail = msg;
                ms.Retries = 0; ms.NotificationType = NotificationType.SMS;
                ms.NotificationStatus = NotificationStatus.NotSent;
                _context.MailSystems.Add(ms);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

            }

            return RedirectToPage("./Details", new { id = usertimesheet.Id });
        }

        private bool UserTimeSheetExists(int id)
        {
            return (_context.UserTimeSheets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
