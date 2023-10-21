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
        /*[HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var model = new RecipeViewModel
            {
                Recipe = _context.Recipes.Find(id),
                Amounts = _context.RecipeIngredients.Where(ri => ri.RecipeID == id).Select(ri => ri.Amount).ToList(),
                IngredientNames = _context.RecipeIngredients.Include(ri => ri.Ingredient).Where(ri => ri.RecipeID == id).Select(ri => ri.Ingredient.IngredientName).ToList(),
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(RecipeViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach(RecipeIngredient recipeIngredient in model.RecipeIngredients)
                {
                    _context.RecipeIngredients.Add(recipeIngredient);
                }
                _context.Recipes.Add(model.Recipe);
                _context.SaveChanges();
                return RedirectToAction("ChooseRecipe", "Home");
            } else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                ViewBag.Action = "Edit";
                return RedirectToAction("ChooseRecipe", "Home");
            }
        }*/
        public IActionResult RecipeView(int id = 1) 
        {
            var theModel =  _context.Recipes.Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient).FirstOrDefault(r => r.RecipeID == id);
            return View(theModel);
        }

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