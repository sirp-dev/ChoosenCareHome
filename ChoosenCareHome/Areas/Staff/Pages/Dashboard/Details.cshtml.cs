using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static ChoosenCareHome.Data.Model.Enum;
using System.Net.Mail;

namespace ChoosenCareHome.Areas.Staff.Pages.Dashboard
{
    [Authorize]

    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnv;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }
        [BindProperty]
        public UserTimeSheet UserTimeSheet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserTimeSheets == null)
            {
                return NotFound();
            }

            var usertimesheet = await _context.UserTimeSheets
                .Include(x=>x.TimeSheet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usertimesheet == null)
            {
                return RedirectToPage("./TimeSheet");
            }
            else
            {
                UserTimeSheet = usertimesheet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostStartAsync()
        {

            var usertimesheet = await _context.UserTimeSheets
                .Include(x=>x.User)
                .FirstOrDefaultAsync(m => m.Id == UserTimeSheet.Id);
            usertimesheet.UserSheetStartTime = DateTime.UtcNow; 
            _context.Attach(usertimesheet).State = EntityState.Modified;


            await _context.SaveChangesAsync();
            TempData["success"] = "Successful";


            //send email
            string title = "TimeSheet Start Time Update";
            string msg = $"{usertimesheet.User.Surname + usertimesheet.User.FirstName} started the sheet at {usertimesheet.UserSheetStartTime} at {usertimesheet.PostCode}";

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

        public async Task<IActionResult> OnPostEndAsync()
        {

            var usertimesheet = await _context.UserTimeSheets.Include(x => x.User).FirstOrDefaultAsync(m => m.Id == UserTimeSheet.Id);
            usertimesheet.UserSheetEndTime = DateTime.UtcNow;
            _context.Attach(usertimesheet).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            TempData["success"] = "Successful";


            //send email
            string title = "TimeSheet End Time Update";
            string msg = $"{usertimesheet.User.Surname + usertimesheet.User.FirstName} started the sheet at {usertimesheet.UserSheetEndTime} at {usertimesheet.PostCode}";

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
    }

}
