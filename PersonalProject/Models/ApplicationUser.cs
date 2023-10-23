using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        ICollection<Recipe>? Recipes { get; set; }
        [NotMapped]
        public IList<string> RoleNames { get; set; }
    }
}
