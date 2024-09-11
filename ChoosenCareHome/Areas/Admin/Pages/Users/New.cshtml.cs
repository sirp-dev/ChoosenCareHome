
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChoosenCareHome.Areas.Admin.Pages.Users
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class NewModel : PageModel
    {
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager; 
        private readonly ILogger<NewModel> _logger; 
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public NewModel(
            UserManager<Profile> userManager, 
            SignInManager<Profile> signInManager,
            ILogger<NewModel> logger, 
            Data.ApplicationDbContext context)
        {
            _userManager = userManager; 
            _signInManager = signInManager;
            _logger = logger; 
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public Profile Input { get; set; }

        [BindProperty]
        public int AppId { get; set; }

        [BindProperty]
        public Application Application { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
             
            Application = await _context.Applications.FindAsync(id);
            //if(Application == null)
            //{
            //    return NotFound();
            //}

            // ViewData["ApplicationId"] = new SelectList(_context.Applications, "Id", "Email");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            var user = new Profile
            {
                UserName = Input.Email,
                Email = Input.Email,
                PhoneNumber = Input.PhoneNumber,
                FirstName = Input.FirstName,
                MiddleName = Input.MiddleName,
                ChangePass = true,
                Date = DateTime.UtcNow,
                UserStatus = Data.Model.Enum.UserStatus.Active,
                EmailConfirmed = true,
                Role = Input.Role,
            };
            if (AppId != 0)
            {
                user.ApplicationId = AppId;
            }
            user.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, Input.PhoneNumber);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                return RedirectToPage("Index");

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            Application = await _context.Applications.FindAsync(AppId);

            return Page();
        }

    }
}