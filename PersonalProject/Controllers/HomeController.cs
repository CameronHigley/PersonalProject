using Microsoft.AspNetCore.Mvc;
using PersonalProject.Data;
using PersonalProject.Models;
using System.Diagnostics;

namespace PersonalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult RecipeView(int id = 1) 
        {
            var theModel = new RecipeViewModel
            {
                Recipe = _context.Recipes.Find(id),
                Ingredients = _context.Ingredients
                    .Where(i => i.RecipeID == id)
                    .ToList(),
                Instructions = _context.Instructions
                    .Where(i => i.RecipeID == id)
                    .ToList(),
            };
            return View(theModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}