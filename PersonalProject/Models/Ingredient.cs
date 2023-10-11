namespace PersonalProject.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }
        public string IngredientAmount { get; set; }

        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; } = null!;
    }
}
