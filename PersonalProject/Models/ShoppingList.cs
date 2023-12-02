using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalProject.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "You must be logged in")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<ShoppingListIngredient> ShoppingListIngredients { get; set; }
    }
}
