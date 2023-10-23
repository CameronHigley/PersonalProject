using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalProject.Data;
using PersonalProject.Models;

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
    }
}
