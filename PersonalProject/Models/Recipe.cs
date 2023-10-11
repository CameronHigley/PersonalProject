using Microsoft.Extensions.Hosting;

namespace PersonalProject.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }


        public ICollection<Ingredient> Ingredients { get; } = new List<Ingredient>();
        public ICollection<Instruction> Instructions { get; } = new List<Instruction>();
    }
}
