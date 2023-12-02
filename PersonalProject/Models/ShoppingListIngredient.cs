namespace PersonalProject.Models
{
    public class ShoppingListIngredient
    {
        public int ShoppingListId { get; set; }
        public int IngredientId { get; set; }
        public ShoppingList? ShoppingList { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
