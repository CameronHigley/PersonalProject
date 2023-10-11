using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalProject.Models;

namespace PersonalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set;}
        public DbSet<Instruction> Instructions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    RecipeID = 1,
                    RecipeName = "Test",

                }
                );
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    IngredientID = 1,
                    IngredientName = "Chicken",
                    IngredientAmount = "1 lb.",
                    RecipeID = 1
                },
                new Ingredient
                {
                    IngredientID = 2,
                    IngredientName = "Salt",
                    IngredientAmount = "1 tsp",
                    RecipeID = 1
                }
                );
            modelBuilder.Entity<Instruction>().HasData(
                new Instruction
                {
                    InstructionID = 1,
                    InstructionDescription = "Add salt to chicken and cook until done.",
                    RecipeID = 1
                }
                );
            
        }
    }
}