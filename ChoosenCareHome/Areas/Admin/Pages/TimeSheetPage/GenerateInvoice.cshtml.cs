using ChoosenCareHome.Data.Model;
using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    public class GenerateInvoiceModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public GenerateInvoiceModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<UserTimeSheet> UserTimeSheets { get; set; }
        public Profile UserProfile { get; set; }
        public string Fullname { get; set; }


        [BindProperty]
        public Invoice Invoice { get; set; }
        public List<UserListDto> UserList { get; set; }
        public string MonthYearTitle { get; set; }

        [BindProperty]
        public int Year { get; set; }

        [BindProperty]
        public int Month { get; set; }
        public async Task<IActionResult> OnGetAsync(string id, int year, int month)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            UserProfile = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (UserProfile == null)
            {
                return NotFound();
            }

            Year = year;
            Month = month;

            MonthYearTitle = new DateTime(year, month, 1).ToString("MMMM yyyy");
            Fullname = UserProfile.FirstName + " " + UserProfile.Surname;
            UserTimeSheets = await _context.UserTimeSheets.Include(x => x.TimeSheet)
                .Where(x => x.UserId == id && x.TimeSheet.Date.Year == year && x.TimeSheet.Date.Month == month)
                 
                .ToListAsync();


            //var totalHours = (item.EndTime - item.StartTime).TotalHours - (item.Break);
            if (UserTimeSheets.Any())
            {
                Invoice = new Invoice();
                Invoice.PeriodStart = UserTimeSheets.Min(uts => uts.Date);
                Invoice.PeriodEnd = UserTimeSheets.Max(uts => uts.Date);
                Invoice.Rate = UserTimeSheets.First().RatePerHour; // Assuming the rate is consistent across sheets
                Invoice.TotalHours = Convert.ToDecimal(UserTimeSheets.Sum(uts => (uts.EndTime - uts.StartTime).TotalHours - (uts.Break)));
                //Invoice.TotalHours = 0m; // Initialize total hours

                //foreach (var item in UserTimeSheets)
                //{
                //    Invoice.TotalHours += Convert.ToDecimal((item.EndTime - item.StartTime).TotalHours) - item.Break;
                //}
                Invoice.TotalPay = Invoice.TotalHours * Invoice.Rate;
                Invoice.NetPay = Invoice.TotalPay - Invoice.IncomeTax - Invoice.NationalInsurance;

            }

            //foreach (var invoice in UserTimeSheets) {
            //    Invoice.TotalHours = Convert.ToDecimal(invoice.EndTime - invoice.StartTime) - (invoice.Break);
            //}

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var checkinvoice = await _context.Invoices.FirstOrDefaultAsync(x => x.InvoiceDate.Month == DateTime.UtcNow.AddHours(1).Month && 
            x.UserId == Invoice.UserId);
            if (checkinvoice == null)
            {


                try
                {

                    Invoice.TotalPay = Invoice.TotalHours * Invoice.Rate;
                    Invoice.NetPay = Invoice.TotalPay - Invoice.IncomeTax - Invoice.NationalInsurance;
                    Invoice.InvoiceDate = DateTime.UtcNow.AddHours(1);
                    Invoice.InvoiceStatus = Data.Model.Enum.InvoiceStatus.Pending;
                    _context.Invoices.Add(Invoice);
                    await _context.SaveChangesAsync();
                    var xUserTimeSheets = await _context.UserTimeSheets.AsNoTracking()
                   .FirstOrDefaultAsync(x => x.UserId == Invoice.UserId && x.TimeSheet.Date.Year == Year && x.TimeSheet.Date.Month == Month);
                    
                    if(xUserTimeSheets != null)
                    {
                        xUserTimeSheets.InvoiceNumber = Invoice.Id.ToString("0000");
                        xUserTimeSheets.GeneratedInvoice = true;
                        _context.Attach(xUserTimeSheets).State = EntityState.Modified;
                    }


                    await _context.SaveChangesAsync();

                    var getInvoice = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == Invoice.Id);
                    getInvoice.InvoiceNumber = Invoice.Id.ToString("0000");
                    _context.Attach(getInvoice).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Invoice", new { id = Invoice.Id });
                }
                catch (Exception ex)
                {
                    TempData["error"] = "unable to generate invoice. try again";
                    return Page();
                }
            }
            else
            {
                checkinvoice.PeriodStart = Invoice.PeriodStart;
                checkinvoice.PeriodEnd = Invoice.PeriodEnd;
                checkinvoice.Rate = Invoice.Rate;
                checkinvoice.TotalHours = Invoice.TotalHours;
                checkinvoice.TotalPay = Invoice.TotalPay;
                checkinvoice.NetPay = Invoice.NetPay;
                checkinvoice.IncomeTax = Invoice.IncomeTax;
                checkinvoice.NationalInsurance = Invoice.NationalInsurance;
                checkinvoice.NINumber = Invoice.NINumber;
                checkinvoice.P45 = Invoice.P45;
                checkinvoice.TaxCode = Invoice.TaxCode;
                checkinvoice.PaymentMethod = Invoice.PaymentMethod; 

                checkinvoice.TotalPay = Invoice.TotalHours * Invoice.Rate;
                checkinvoice.NetPay = Invoice.TotalPay - Invoice.IncomeTax - Invoice.NationalInsurance;
                checkinvoice.InvoiceDate = DateTime.UtcNow.AddHours(1);

                _context.Attach(checkinvoice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToPage("./Invoice", new { id = checkinvoice.Id });
            }

        }
    }
}
