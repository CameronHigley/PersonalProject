using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalProject.Data;
using PersonalProject.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace PersonalProject.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;
        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._context = context;
        }
        public IActionResult AllRecipes()
        {
            ChooseRecipeViewModel model = new ChooseRecipeViewModel
            {
                Recipes = _context.Recipes.Include(r => r.User).ToList(),
            };
            return View(model);
        }

        public IActionResult RecipeView(int id)
        {
            Recipe model = _context.Recipes.Include(r => r.User)
                                            .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                                            .FirstOrDefault(r => r.RecipeID == id);
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllUsers()
        {
            IList<ApplicationUser> model = new List<ApplicationUser>();
            foreach (ApplicationUser user in userManager.Users)
            {
                model.Add(user);
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            await userManager.DeleteAsync(await userManager.FindByIdAsync(id));
            return RedirectToAction("AllUsers");
        }

        [HttpGet]
        public IActionResult DeleteRecipe(int id)
        {
            Recipe model = _context.Recipes.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteRecipe(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            return RedirectToAction("AllRecipes");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new RecipeViewModel
            {
                Recipe = _context.Recipes.Find(id),
                RecipeIngredients = _context.RecipeIngredients.Include(ri => ri.Ingredient).Where(ri => ri.RecipeID == id).ToList(),
                Ingredients = new List<Ingredient>(),
            };
            foreach (RecipeIngredient ri in model.RecipeIngredients)
            {
                Ingredient ingredient = _context.Ingredients.Find(ri.IngredientID);
                model.Ingredients.Add(ingredient);
            }
            ViewBag.userId = model.Recipe.UserId;
            return View(model);

        }
        [HttpPost]
        public IActionResult Edit(RecipeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Recipes.Update(model.Recipe);
                List<RecipeIngredient> oldRecipeIngredients = _context.RecipeIngredients.Where(ri => ri.RecipeID == model.Recipe.RecipeID).ToList();
                List<RecipeIngredient> currentRecipeIngredients = new List<RecipeIngredient>();
                for (int i = 0; i < model.Ingredients.Count(); i++)
                {
                    var name = model.Ingredients[i].IngredientName;
                    var ingredient = new Ingredient();
                    if (_context.Ingredients.Any(i => i.IngredientName == name))
                    {
                        ingredient = _context.Ingredients.FirstOrDefault(i => i.IngredientName == name);
                    }
                    else
                    {
                        ingredient = new Ingredient
                        {
                            IngredientName = name,
                        };
                        _context.Ingredients.Add(ingredient);
                    }
                    var recipeIngredient = new RecipeIngredient
                    {
                        Recipe = model.Recipe,
                        RecipeID = model.Recipe.RecipeID,
                        Ingredient = ingredient,
                        IngredientID = ingredient.IngredientID,
                        Amount = model.RecipeIngredients[i].Amount
                    };
                    // if ingredient is already linked to this recipe
                    if (_context.RecipeIngredients.Any(ri => ri.RecipeID == recipeIngredient.RecipeID
                        && ri.IngredientID == recipeIngredient.IngredientID))
                    {
                        RecipeIngredient oldRecipeIngredient = _context.RecipeIngredients.Where(ri => ri.RecipeID == recipeIngredient.RecipeID).FirstOrDefault(ri => ri.IngredientID == recipeIngredient.IngredientID);
                        //check if amount is different
                        if (oldRecipeIngredient.Amount != recipeIngredient.Amount)
                        {
                            _context.RecipeIngredients.Update(recipeIngredient);
                        }
                    }
                    else
                    {
                        _context.RecipeIngredients.Add(recipeIngredient);
                    }
                    currentRecipeIngredients.Add(recipeIngredient);
                }
                foreach (RecipeIngredient oldri in oldRecipeIngredients)
                {
                    if (!currentRecipeIngredients.Any(ri => ri.RecipeID == oldri.RecipeID && ri.IngredientID == oldri.IngredientID))
                    {
                        _context.RecipeIngredients.Remove(oldri);
                    }
                }
                _context.SaveChanges();
                return RedirectToAction("AllRecipes");
            }
            else
            {
                ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View(model);
            }
        }
    }
}
