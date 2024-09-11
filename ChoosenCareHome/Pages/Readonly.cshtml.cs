using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChoosenCareHome.Pages
{
     public class ReadonlyModel : PageModel
    {
        private readonly SignInManager<Profile> _signInManager;
        private readonly RoleManager<AppRole> _role;
        private readonly UserManager<Profile> _userManager;
        private readonly ILogger<ReadonlyModel> _logger;
         private readonly ApplicationDbContext _context;


        public ReadonlyModel(
            UserManager<Profile> userManager,
            SignInManager<Profile> signInManager,
            RoleManager<AppRole> role,
        ILogger<ReadonlyModel> logger,
            ApplicationDbContext context )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _role = role;
             _context = context;
        }
        [BindProperty]
        public string RefId { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public string Phone { get; set; }
 
        }

        // public string REFID { get; set; }
        public async Task OnGetAsync(string returnUrl = null, string refxid = null)
        {

            if (ModelState.IsValid)
            {
                var user = new Profile
                {
                    UserName = "info@chosenhealthcare.co.uk",
                    Email = "info@chosenhealthcare.co.uk",
                     
                    EmailConfirmed = true,
                     
                };
                
                user.Id = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, "Care@2023");
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //IdentityRole JAdmin = new IdentityRole("Admin");
                    //var checkJAdmin = await _role.FindByNameAsync("Admin");
                    //if (checkJAdmin == null)
                    //{
                    //    await _role.CreateAsync(JAdmin);

                    //}

                    await _userManager.AddToRoleAsync(user, "Admin");
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }


        }
    }
}