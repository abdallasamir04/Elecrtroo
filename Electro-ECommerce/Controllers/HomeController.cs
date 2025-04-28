using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electro_ECommerce.Models;
using Electro_ECommerce.ViewModels;
using Electro_ECommerce.Data;
using static Electro_ECommerce.ViewModels.ProductViewModel;
using static Electro_ECommerce.ViewModels.ProductDetailsViewModel;

namespace Electro_ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly TechXpressDbContext _context;

        public HomeController(TechXpressDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            var productViewModels = products.Select(ProductViewModel.FromProduct).ToList();

            return View(productViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews) // Include Reviews here
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Get related products (same category)
            var relatedProducts = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != product.ProductId)
                .Take(4)
                .ToListAsync();

            var relatedProductViewModels = relatedProducts.Select(ProductViewModel.FromProduct).ToList();

            var productDetailsViewModel = ProductDetailsViewModel.FromProduct(product, relatedProductViewModels);
            // Map product reviews to ReviewViewModel
            productDetailsViewModel.Reviews = product.Reviews.Select(r => new ProductDetailsViewModel.ReviewViewModel
            {
                UserName = r.UserName,
                Rating = r.Rating,
                Comment = r.Comment,
                Date = r.Date,
                ProductId = r.ProductId
            }).ToList();

            return View(productDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(string userName, string email, int rating, string comment, int productId)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(comment) || rating < 1 || rating > 5)
            {
                TempData["ReviewError"] = "Please fill in all required fields correctly.";
                return RedirectToAction("Details", new { id = productId });
            }

            var newReview = new Review
            {
                UserName = userName,
                Rating = rating,
                Comment = comment,
                Date = DateTime.Now,
                ProductId = productId
            };

            _context.Reviews.Add(newReview);
            await _context.SaveChangesAsync();

            TempData["ReviewSuccess"] = "Your review has been submitted successfully!";
            return RedirectToAction("Details", new { id = productId });
        }

        [HttpPost]
        public async Task<IActionResult> FilterProducts([FromBody] FilterProductsRequest request)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            // Filter by categories
            if (request.Categories != null && request.Categories.Length > 0 && !request.Categories.Contains("all"))
            {
                query = query.Where(p => request.Categories.Contains(p.Category != null ? p.Category.Name : string.Empty));
            }

            // Filter by price range
            query = query.Where(p => p.Price >= request.MinPrice && p.Price <= request.MaxPrice);

            // Filter by status
            if (request.Status != null && request.Status.Length > 0)
            {
                if (request.Status.Contains("sale"))
                {
                    // Assuming products on sale have a discount
                    query = query.Where(p => p.DiscountPercentage > 0);
                }
                if (request.Status.Contains("new"))
                {
                    // Assuming new products are added within the last 30 days
                    var thirtyDaysAgo = DateTime.Now.AddDays(-30);
                    query = query.Where(p => p.CreatedAt >= thirtyDaysAgo);
                }
                if (request.Status.Contains("instock"))
                {
                    query = query.Where(p => p.StockQuantity > 0);
                }
            }

            var products = await query.ToListAsync();
            var productViewModels = products.Select(ProductViewModel.FromProduct).ToList();

            return Json(productViewModels);
        }

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction(nameof(Index));
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(query) ||
                           (p.Description != null && p.Description.Contains(query)) ||
                           (p.Category != null && p.Category.Name.Contains(query)))
                .ToListAsync();

            var productViewModels = products.Select(ProductViewModel.FromProduct).ToList();

            ViewBag.SearchQuery = query;
            return View("SearchResults", productViewModels);
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

    public class FilterProductsRequest
    {
        public string[]? Categories { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string[]? Status { get; set; }
    }
}
