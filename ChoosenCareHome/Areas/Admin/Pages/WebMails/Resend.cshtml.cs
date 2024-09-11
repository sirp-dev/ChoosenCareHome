using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using ChoosenCareHome.Data.Model;
using static ChoosenCareHome.Data.Model.Enum;

namespace ChoosenCareHome.Areas.Admin.Pages.WebMails
{
    public class ResendModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public ResendModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public MailSystem i { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            i = await _context.MailSystems.FirstOrDefaultAsync(m => m.Id == id);

            if (i == null)
            {
                return NotFound();
            }
            if (i.NotificationType != NotificationType.SMS)
            {
                //
                string result = await SendEmail(i.Email, i.Mail, i.Title);
                if (result == "true" )
                {
                    i.NotificationStatus = NotificationStatus.Sent;
                    i.Result = i.Result + "<br><br><br>"+ result;
                    TempData["sendt"] = "Sent";
                }
                else
                {
                    i.NotificationStatus = NotificationStatus.NotSent;
                    i.Retries = i.Retries + 1;
                    i.Result = i.Result + "<br><br><br>" + result;
                    TempData["sendt"] = "failed";
                }
            }

            try
            {

                var iod = await _context.MailSystems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == i.Id);
                iod.NotificationStatus = i.NotificationStatus;
                iod.Retries = i.Retries;
                //_context.Entry(iod).State = EntityState.Detached;
                _context.Attach(iod).State = EntityState.Modified;


            }

            catch (Exception webex)
            {

            }


        
        await _context.SaveChangesAsync();
            
            return Page();
        }

        public async Task<string> SendEmail(string recipient, string message, string title)
        {
            try
            {
                //create the mail message 
                MailMessage mail = new MailMessage();
                mail.Body = message;
                //set the addresses 
                mail.From = new MailAddress("", ""); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(recipient);

                //set the content 
                mail.Subject = title.Replace("\r\n", "");

                mail.IsBodyHtml = true;
                //send the message 
                SmtpClient smtp = new SmtpClient("");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("", "");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 25;    //alternative port number is 8889
                smtp.EnableSsl = false;
                smtp.Send(mail);
                return "true";
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
        }

    }
}
