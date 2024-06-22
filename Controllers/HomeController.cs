using kontorExpert.BusinessLogic;
using kontorExpert.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace kontorExpert.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryLogic _categoryLogic;

        public HomeController(ILogger<HomeController> logger, CategoryLogic categoryLogic)
        {
            _logger = logger;
            _categoryLogic = categoryLogic;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryLogic.GetAllCategoriesAsync();
            return View(categories);
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
