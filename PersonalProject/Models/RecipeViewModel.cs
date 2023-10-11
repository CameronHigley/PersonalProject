namespace PersonalProject.Models
{
    public class RecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public List<Ingredient> Ingredients { get; set;}
        public List<Instruction> Instructions { get; set; }
    }
}
