using Microsoft.AspNetCore.Identity;

namespace ChoosenCareHome.Data.Model
{
    public class AppRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
