namespace PersonalProject.Models
{
    public class RecipeIngredient
    {
        public int RecipeID { get; set; }
        public int IngredientID { get; set; }
        public string Amount { get; set; }

        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}
