using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Electro_ECommerce.Models;
using Electro_ECommerce.ViewModels;
using Electro_ECommerce.Data;

namespace Electro_ECommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly TechXpressDbContext _context;
        private readonly UserManager<User> _userManager;

        public ShoppingCartController(TechXpressDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Cart()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                // Handle anonymous cart (could be stored in session or cookies)
                return View(new List<ShoppingCart>());
            }

            var cartItems = await _context.ShoppingCarts
                .Include(c => c.Product)
                .ThenInclude(p => p.Category)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Check if product is in stock
            if (product.StockQuantity < quantity)
            {
                TempData["ErrorMessage"] = "Not enough stock available.";
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return BadRequest(new { success = false, message = "Not enough stock available." });
                }
                return RedirectToAction("Details", "Home", new { id = productId });
            }

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                // Redirect to login with return URL
                return RedirectToAction("Login", "Account", new { returnUrl = Request.Headers["Referer"].ToString() });
            }

            // Check if item already exists in cart
            var cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                // Update existing cart item
                cartItem.Quantity += quantity;
                cartItem.UpdatedAt = DateTime.Now;
                _context.ShoppingCarts.Update(cartItem);
            }
            else
            {
                // Create new cart item
                cartItem = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.ShoppingCarts.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product added to cart!";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Ok(new { success = true, message = "Product added to cart!" });
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItem = await _context.ShoppingCarts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.CartId == cartId && c.UserId == userId);

            if (cartItem == null)
            {
                return NotFound();
            }

            if (quantity <= 0)
            {
                _context.ShoppingCarts.Remove(cartItem);
            }
            else
            {
                // Check if requested quantity is available
                if (quantity > cartItem.Product.StockQuantity)
                {
                    TempData["ErrorMessage"] = "Not enough stock available.";
                    return RedirectToAction(nameof(Cart));
                }

                cartItem.Quantity = quantity;
                cartItem.UpdatedAt = DateTime.Now;
                _context.ShoppingCarts.Update(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.CartId == cartId && c.UserId == userId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.ShoppingCarts.Remove(cartItem);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Item removed from cart!";
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = await _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.ShoppingCarts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cart cleared!";
            return RedirectToAction(nameof(Cart));
        }
    }
}
