using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChoosenCareHome.Areas.Admin.Pages.Users
{
 
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class ResetPasswordModel : PageModel
    {
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;
        private readonly ILogger<NewModel> _logger;
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public ResetPasswordModel(
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
        [BindProperty]
        public string Password { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(Profile.Id);
            
            var rempass = await _userManager.RemovePasswordAsync(user);
            if(rempass.Succeeded)
            {
                var addpass = await _userManager.AddPasswordAsync(user, Password);
                if(addpass.Succeeded)
                {
                    TempData["success"] = "successful";
                    return Page();
                }
            }
            TempData["error"] = "failed";

            return Page();
        }

    }
}
