using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Electro_ECommerce.Models;
using Electro_ECommerce.ViewModels;
using Electro_ECommerce.Data;

namespace Electro_ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly TechXpressDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(
            TechXpressDbContext context,
            UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            // Get counts for dashboard
            ViewBag.ProductsCount = await _context.Products.CountAsync();
            ViewBag.UsersCount = await _userManager.Users.CountAsync();
            ViewBag.OrdersCount = await _context.Orders.CountAsync();
            ViewBag.Revenue = await _context.Orders.SumAsync(o => o.TotalAmount);

            // Get recent orders
            ViewBag.RecentOrders = await _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToListAsync();

            // Get top products
            ViewBag.TopProducts = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.OrderDetails.Count)
                .Take(5)
                .ToListAsync();

            return View();
        }

        #region Product Management
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.ProductId)
                .ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload
                if (image != null && image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "products");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    product.ImagePath = $"/uploads/products/{fileName}";
                }

                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product created successfully!";
                return RedirectToAction(nameof(Products));
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(product);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = await _context.Products.FindAsync(product.ProductId);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Handle image upload
                    if (image != null && image.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "products");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        // Delete old image if exists
                        if (!string.IsNullOrEmpty(existingProduct.ImagePath))
                        {
                            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingProduct.ImagePath.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        existingProduct.ImagePath = $"/uploads/products/{fileName}";
                    }

                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.StockQuantity = product.StockQuantity;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.UpdatedAt = DateTime.Now;

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction(nameof(Products));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Delete product image if exists
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product deleted successfully!";
            return RedirectToAction(nameof(Products));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        #endregion

        #region Category Management
        public async Task<IActionResult> Categories()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.Now;
                category.UpdatedAt = DateTime.Now;

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction(nameof(Categories));
            }

            return View(category);
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCategory = await _context.Categories.FindAsync(category.CategoryId);
                    if (existingCategory == null)
                    {
                        return NotFound();
                    }

                    existingCategory.Name = category.Name;
                    existingCategory.Description = category.Description;
                    existingCategory.UpdatedAt = DateTime.Now;

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Category updated successfully!";
                    return RedirectToAction(nameof(Categories));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Check if category has products
            var hasProducts = await _context.Products.AnyAsync(p => p.CategoryId == id);
            if (hasProducts)
            {
                TempData["ErrorMessage"] = "Cannot delete category because it has associated products.";
                return RedirectToAction(nameof(Categories));
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Category deleted successfully!";
            return RedirectToAction(nameof(Categories));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
        #endregion

        #region Order Management
        // GET: Admin/Orders
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: Admin/OrderDetails/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.User)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View("OrderDetails", order); // uses Views/Admin/OrderDetails.cshtml
        }

        // POST: Admin/UpdateOrderStatus
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;
            order.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Order status updated successfully.";
            return RedirectToAction(nameof(OrderDetails), new { id = orderId });
        }
        #endregion
    }
}
