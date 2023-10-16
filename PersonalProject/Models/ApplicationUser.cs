using Microsoft.AspNetCore.Identity;

namespace PersonalProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        ICollection<Recipe>? Recipes { get; set; }
    }
}
