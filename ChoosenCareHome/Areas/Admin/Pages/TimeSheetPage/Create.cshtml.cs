﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class CreateModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public CreateModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TimeSheet TimeSheet { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            DateTime currentDate = DateTime.Now;
            DateTime endDate = new DateTime(2024, 12, 31);

            while (currentDate <= endDate)
            {
                TimeSheet newTimeSheet = new TimeSheet
                {
                    Date = currentDate,
                    // Assuming Id is auto-generated by the database
                };

                _context.TimeSheets.Add(newTimeSheet);
                currentDate = currentDate.AddDays(1); // Move to the next day
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
