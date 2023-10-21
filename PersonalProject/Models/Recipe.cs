using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace PersonalProject.Models
{
    public class Recipe
    {
        
        public int RecipeID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? RecipeName { get; set; }
        [Required(ErrorMessage = "Instructions are required.")]
        public string? RecipeInstructions { get; set; }

        public ICollection<RecipeIngredient>? RecipeIngredients { get; set;}

        [Required(ErrorMessage = "You must be logged in.")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
