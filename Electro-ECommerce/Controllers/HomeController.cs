
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Electro_ECommerce.Models;
using Electro_ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using Electro_ECommerce.ViewModels;

namespace Electro_ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TechXpressDbContext _context;

        public HomeController(ILogger<HomeController> logger, TechXpressDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    OriginalPrice = p.DiscountPercentage > 0 ? p.Price * (1 + p.DiscountPercentage / 100) : (decimal?)null,
                    CategoryName = p.Category.Name,
                    ImagePath = p.ImagePath,
                    IsNew = (DateTime.Now - p.CreatedAt).TotalDays <= 30,
                    IsOnSale = p.DiscountPercentage > 0
                })
                .ToListAsync();

            ViewBag.Categories = await _context.Categories.Take(8).ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Get related products
            var relatedProducts = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != id)
                .Take(4)
                .Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    OriginalPrice = p.DiscountPercentage > 0 ? p.Price * (1 + p.DiscountPercentage / 100) : (decimal?)null,
                    CategoryName = p.Category.Name,
                    ImagePath = p.ImagePath,
                    IsNew = (DateTime.Now - p.CreatedAt).TotalDays <= 30,
                    IsOnSale = p.DiscountPercentage > 0
                })
                .ToListAsync();

            var viewModel = ProductDetailsViewModel.FromProduct(product, relatedProducts);

            // Map reviews
            if (product.Reviews != null)
            {
                viewModel.Reviews = product.Reviews.Select(r => new ProductDetailsViewModel.ReviewViewModel
                {
                    UserName = r.UserName,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Date = r.Date,
                    ProductId = r.ProductId
                }).ToList();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(int productId, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
            {
                TempData["ErrorMessage"] = "Rating must be between 1 and 5.";
                return RedirectToAction(nameof(Details), new { id = productId });
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Create a new review
            var review = new Review
            {
                ProductId = productId,
                UserName = User.Identity.IsAuthenticated ? User.Identity.Name : "Guest",
                Rating = rating,
                Comment = comment,
                Date = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your review has been submitted successfully.";
            return RedirectToAction(nameof(Details), new { id = productId });
        }

        public async Task<IActionResult> Search(string query, int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews) // Include Reviews in the query
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(query))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(query) ||
                    p.Description.Contains(query) ||
                    p.Category.Name.Contains(query));
            }

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price <= maxPrice.Value);
            }

            var products = await productsQuery
                .Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    OriginalPrice = p.DiscountPercentage > 0 ? p.Price * (1 + p.DiscountPercentage / 100) : (decimal?)null,
                    CategoryName = p.Category.Name,
                    ImagePath = p.ImagePath,
                    IsNew = (DateTime.Now - p.CreatedAt).TotalDays <= 30,
                    IsOnSale = p.DiscountPercentage > 0,
                })
                .ToListAsync();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Query = query;
            ViewBag.CategoryId = categoryId;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            return View("SearchResults", products);
        }


        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> QuickView(int productId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound();
            }

            return PartialView("QuickView", product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

