using Microsoft.Extensions.Hosting;

namespace PersonalProject.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public string RecipeInstructions { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set;}
        
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
