using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalProject.Data;
using PersonalProject.Models;
using System.Diagnostics;

namespace PersonalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        //private UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context/*, UserManager<ApplicationUser> userManager*/)
        {
            _logger = logger;
            _context = context;
            //_userManager = userManager;
        }

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

        /*public async Task<IActionResult> ChooseRecipeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new ChooseRecipeViewModel
            {
                Recipes = _context.Recipes.Where(r => r.UserId == user.Id).ToList()
            };

            return View(model);
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}