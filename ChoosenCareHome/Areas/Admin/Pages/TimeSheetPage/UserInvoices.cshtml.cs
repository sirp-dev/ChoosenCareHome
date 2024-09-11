using ChoosenCareHome.Data.Model;
using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    public class UserInvoicesModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public UserInvoicesModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        public List<InvoiceGroupViewModel> InvoiceGroupViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var year = DateTime.Now.Year; // Assuming you want the current year. Adjust as necessary.

            var groupedInvoices = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.InvoiceDate.Year == year)
                .GroupBy(i => i.InvoiceDate.Month)
                .Select(g => new InvoiceGroupViewModel
                {
                    Year = year,
                    Month = g.Key,
                    Count = g.Count(),
                    UniqueUserCount = g.Select(i => i.UserId).Distinct().Count()
                })
                .ToListAsync();

            // Ensure all months are present
            var allMonths = Enumerable.Range(1, 12).Select(m => new InvoiceGroupViewModel
            {
                Year = year,
                Month = m,
                Count = 0,
                UniqueUserCount = 0
            }).ToList();

            foreach (var invoiceGroup in groupedInvoices)
            {
                var month = allMonths.First(m => m.Month == invoiceGroup.Month);
                month.Count = invoiceGroup.Count;
                month.UniqueUserCount = invoiceGroup.UniqueUserCount;
            }

            InvoiceGroupViewModel = allMonths.OrderBy(m => m.Month).ToList();
            return Page();
        }
    }

    public class InvoiceGroupViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
        public int UniqueUserCount { get; set; }
    }
}