using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using System.Globalization;

namespace ChoosenCareHome.Areas.Admin.Pages.RotaPage
{
    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public UserRota UserRota { get; set; } = default!;
        public List<UserTimeSheet> UserTimeSheets { get; set; } = default!;
        public string DateTitle { get; set; } = string.Empty;
        public int CurrentYear { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentWeek { get; set; }
        public int CurrentDay { get; set; }

        public int? RouteYear { get; set; }
        public int? RouteMonth { get; set; }
        public int? RouteWeek { get; set; }
        public int? RouteDay { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? day, int? week, int? month, int? year)
        {
            RouteYear = year;
            RouteMonth = month;
            RouteWeek = week;
            RouteDay = day;


            if (id == null || _context.UserRotas == null)
            {
                return NotFound();
            }

            var userclient = await _context.UserRotas.FirstOrDefaultAsync(m => m.Id == id);
            if (userclient == null)
            {
                return NotFound();
            }
            else
            {
                UserRota = userclient;
            }

            IQueryable<UserTimeSheet> query = _context.UserTimeSheets
         .Include(x => x.User)
         .Include(x => x.TimeSheet)
         .Where(m => m.PostCode == userclient.PostCode && m.TimesheetAcceptance == ChoosenCareHome.Data.Model.Enum.TimesheetAcceptance.Accepted);

            DateTime now = DateTime.Now;

            if (!year.HasValue && !day.HasValue && !month.HasValue && !week.HasValue)
            {
                year = now.Year;
                week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            }
            CurrentYear = year ?? now.Year;
            CurrentMonth = month ?? 0;
            CurrentWeek = week ?? CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            CurrentDay = day ?? 0;
            if (year.HasValue)
            {
                if (day.HasValue)
                {
                    query = query.Where(m => m.TimeSheet.Date.Year == year.Value && m.TimeSheet.Date.Month == month.Value && m.TimeSheet.Date.Day == day.Value);
                    DateTitle = $"Day {now.ToString("dddd dd MMM yyyy")}";
                }
                else if (week.HasValue)
                {
                    DateTime startOfWeek = FirstDateOfWeek(year.Value, week.Value);
                    DateTime endOfWeek = startOfWeek.AddDays(7);
                    query = query.Where(m => m.TimeSheet.Date >= startOfWeek && m.TimeSheet.Date < endOfWeek);
                    DateTitle = $"Week {startOfWeek.ToString("dd MMM yyyy")} - {endOfWeek.ToString("dd MMM yyyy")}";
                }
                else if (month.HasValue)
                {
                    query = query.Where(m => m.TimeSheet.Date.Year == year.Value && m.TimeSheet.Date.Month == month.Value);
                    DateTitle = now.ToString("MMMM yyyy");
                }
                else
                {
                    DateTitle = year.Value.ToString();
                }
            }

            UserTimeSheets = await query.OrderByDescending(x => x.Date).ToListAsync();

            return Page();
        }

        private static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstMonday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if (firstWeek <= 1)
            {
                weekOfYear -= 1;
            }
            return firstMonday.AddDays(weekOfYear * 7);
        }
    }
}
