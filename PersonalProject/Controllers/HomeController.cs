using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalProject.Data;
using PersonalProject.Models;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace PersonalProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
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
            ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
                    if(_context.RecipeIngredients.Any(ri => ri.RecipeID == recipeIngredient.RecipeID 
                        && ri.IngredientID == recipeIngredient.IngredientID))
                    {
                        RecipeIngredient oldRecipeIngredient = _context.RecipeIngredients.Where(ri => ri.RecipeID == recipeIngredient.RecipeID).FirstOrDefault(ri => ri.IngredientID == recipeIngredient.IngredientID);
                        //check if amount is different
                        if(oldRecipeIngredient.Amount != recipeIngredient.Amount)
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
                foreach(RecipeIngredient oldri in oldRecipeIngredients)
                {
                    if (!currentRecipeIngredients.Any(ri => ri.RecipeID == oldri.RecipeID && ri.IngredientID == oldri.IngredientID))
                    {
                        _context.RecipeIngredients.Remove(oldri);
                    }
                }
                _context.SaveChanges();
                return RedirectToAction("ChooseRecipe");
            }
            else
            {
                ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View(model);
            }
        }
        
        public IActionResult RecipeView(int id = 1) 
        {
            var theModel =  _context.Recipes.Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient).FirstOrDefault(r => r.RecipeID == id);
            return View(theModel);
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(new RecipeViewModel());
        }
        [HttpPost]
        public IActionResult Add(RecipeViewModel model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                for(int i = 0;i<model.Ingredients.Count(); i++)
                {
                    var name = model.Ingredients[i].IngredientName;
                    var ingredient = new Ingredient();
                    if(_context.Ingredients.Any(i => i.IngredientName == name))
                    {
                        ingredient = _context.Ingredients.FirstOrDefault(i => i.IngredientName == name);
                    } else
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
                        Ingredient = ingredient,
                        Amount = model.RecipeIngredients[i].Amount
                    };
                    _context.RecipeIngredients.Add(recipeIngredient);
                }
                _context.Recipes.Add(model.Recipe);
                _context.SaveChanges();
                return RedirectToAction("ChooseRecipe");
            } else
            {
                ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recipe = _context.Recipes.Find(id);
            return View(recipe);
        }
        [HttpPost]
        public IActionResult Delete(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            return RedirectToAction("ChooseRecipe");
        }
        public IActionResult ChooseRecipe()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new ChooseRecipeViewModel
            {
                Recipes = _context.Recipes.Where(r => r.UserId == id).ToList()
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}