using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChoosenCareHome.Areas.Admin.Pages.Users
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class UpdateModel : PageModel
    {
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;
        private readonly ILogger<NewModel> _logger;
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public UpdateModel(
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


        [BindProperty]
        public Profile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            Profile = await _userManager.FindByIdAsync(id);
            if (Profile == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(Profile.Id);
            user.Title = Profile.Title;
            user.FirstName = Profile.FirstName;
            user.MiddleName = Profile.MiddleName;
            user.Surname = Profile.Surname;
            user.UserStatus = Profile.UserStatus;
            user.PhoneNumber = Profile.PhoneNumber;
            user.Address = Profile.Address;
            user.DOB = Profile.DOB; 

            await _userManager.UpdateAsync(user);

            TempData["success"] = "successful";
            return RedirectToPage("Index");
        }

    }
}
