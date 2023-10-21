using System.ComponentModel.DataAnnotations;

namespace PersonalProject.Models
{
    public class RecipeViewModel
    {
        [Required]
        public Recipe? Recipe { get; set; }

        public List<RecipeIngredient>? RecipeIngredients { get; set; }

        public List<Ingredient>? Ingredients { get; set;}
    }
}
