using Electro_ECommerce.Data;
using Electro_ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Electro_ECommerce.Controllers
{
    public class HomeController : Controller
    {
        

       
        private readonly TechXpressDbContext _context;
        public HomeController(TechXpressDbContext context)
        {
            _context = context;
        }
        // GET: CategoriesController
        public ActionResult Index()
        {
			var products = from p in _context.Products
						   join c in _context.Categories on p.CategoryId equals c.CategoryId
						   select new
						   {
							   p.ProductId,
							   p.Name,
							   p.Price,
							   p.ImagePath,
							   CategoryName = c.Name 
						   };
			//var items = _context.Products.ToList();
           return View(products);
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
