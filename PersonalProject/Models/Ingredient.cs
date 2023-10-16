namespace PersonalProject.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
