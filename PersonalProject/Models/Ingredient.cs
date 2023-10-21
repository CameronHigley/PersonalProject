using System.ComponentModel.DataAnnotations;

namespace PersonalProject.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        [Required(ErrorMessage = "You must add an ingredient")]
        public string? IngredientName { get; set; }

        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}
