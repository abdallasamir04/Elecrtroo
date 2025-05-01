using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Electro_ECommerce.Models;
using Electro_ECommerce.Data;
using System.Security.Claims;

namespace Electro_ECommerce.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly TechXpressDbContext _context;
        private readonly UserManager<User> _userManager;

        public WishlistController(TechXpressDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account", new { returnUrl = "/Wishlist" });
            }

            var wishlistItems = await _context.Wishlists
                .Include(w => w.Product)
                .ThenInclude(p => p.Category)
                .Where(w => w.UserId == userId)
                .ToListAsync();

            return View(wishlistItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            // Get the current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                // Redirect to login if user is not authenticated
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Wishlist") });
            }

            // Check if the product exists
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return Redirect(Request.Headers["Referer"].ToString() ?? "/");
            }

            // Check if the product is already in the wishlist
            var existingItem = await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (existingItem == null)
            {
                // Add the product to the wishlist
                var wishlistItem = new Wishlist
                {
                    UserId = userId,
                    ProductId = productId,
                    CreatedAt = DateTime.Now
                };

                _context.Wishlists.Add(wishlistItem);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product added to your wishlist!";
            }
            else
            {
                TempData["InfoMessage"] = "This product is already in your wishlist.";
            }

            // Redirect back to the previous page
            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int wishlistId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var wishlistItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.UserId == userId);

            if (wishlistItem == null)
            {
                TempData["ErrorMessage"] = "Wishlist item not found.";
                return RedirectToAction(nameof(Index));
            }

            _context.Wishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product removed from wishlist!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MoveToCart(int wishlistId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var wishlistItem = await _context.Wishlists
                .Include(w => w.Product)
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.UserId == userId);

            if (wishlistItem == null)
            {
                TempData["ErrorMessage"] = "Wishlist item not found.";
                return RedirectToAction(nameof(Index));
            }

            // Check if item already exists in cart
            var cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == wishlistItem.ProductId);

            if (cartItem != null)
            {
                // Update existing cart item
                cartItem.Quantity += 1;
                cartItem.UpdatedAt = DateTime.Now;
                _context.ShoppingCarts.Update(cartItem);
            }
            else
            {
                // Create new cart item
                cartItem = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = wishlistItem.ProductId,
                    Quantity = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.ShoppingCarts.Add(cartItem);
            }

            // Remove from wishlist
            _context.Wishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product moved to cart!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MoveAllToCart()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var wishlistItems = await _context.Wishlists
                .Where(w => w.UserId == userId)
                .ToListAsync();

            if (!wishlistItems.Any())
            {
                TempData["InfoMessage"] = "Your wishlist is empty.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var item in wishlistItems)
            {
                // Check if item already exists in cart
                var cartItem = await _context.ShoppingCarts
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == item.ProductId);

                if (cartItem != null)
                {
                    // Update existing cart item
                    cartItem.Quantity += 1;
                    cartItem.UpdatedAt = DateTime.Now;
                    _context.ShoppingCarts.Update(cartItem);
                }
                else
                {
                    // Create new cart item
                    cartItem = new ShoppingCart
                    {
                        UserId = userId,
                        ProductId = item.ProductId,
                        Quantity = 1,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.ShoppingCarts.Add(cartItem);
                }
            }

            // Remove all items from wishlist
            _context.Wishlists.RemoveRange(wishlistItems);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "All products moved to cart!";
            return RedirectToAction("Cart", "ShoppingCart");
        }
    }
}
