﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalProject.Models;

namespace PersonalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set;}
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListIngredient> ShoppingListIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RecipeIngredient>().HasKey(ri => new { ri.RecipeID, ri.IngredientID });
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeID);
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientID);
            modelBuilder.Entity<ShoppingListIngredient>().HasKey(sli => new { sli.ShoppingListId, sli.IngredientId });
            modelBuilder.Entity<ShoppingListIngredient>()
                .HasOne(sli => sli.ShoppingList)
                .WithMany(sl => sl.ShoppingListIngredients)
                .HasForeignKey(sli => sli.ShoppingListId);
            modelBuilder.Entity<ShoppingListIngredient>()
                .HasOne(sli => sli.Ingredient)
                .WithMany(sl => sl.ShoppingListIngredients)
                .HasForeignKey(sli => sli.IngredientId);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasKey(x => x.Id);
            const string ADMIN_USER_ID = "b4280b6a-0613-4cbd-a9e6-f1701e926e73";
            const string ADMIN_ROLE_ID = ADMIN_USER_ID;
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            });
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_USER_ID,
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_USER_ID
            });
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    RecipeID = 1,
                    RecipeName = "Test",
                    RecipeInstructions = "Add salt to chicken and cook until done.",
                    UserId = "b4280b6a-0613-4cbd-a9e6-f1701e926e73"

                }
                );
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    IngredientID = 1,
                    IngredientName = "Chicken"
                },
                new Ingredient
                {
                    IngredientID = 2,
                    IngredientName = "Salt"
                }
                );
            modelBuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    RecipeID = 1,
                    IngredientID = 1,
                    Amount = "1 lb"
                },
                new RecipeIngredient
                {
                    RecipeID = 1,
                    IngredientID = 2,
                    Amount = "2 tsp"
                }
                );

        }
    }
}