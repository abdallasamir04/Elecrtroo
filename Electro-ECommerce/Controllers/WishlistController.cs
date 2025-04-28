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
                return RedirectToAction("Login", "Account");
            }

            var wishlistItems = await _context.Set<Wishlist>()
                .Include(w => w.Product)
                .ThenInclude(p => p.Category)
                .Where(w => w.UserId == userId)
                .ToListAsync();

            return View(wishlistItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var existingItem = await _context.Set<Wishlist>()
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (existingItem == null)
            {
                // Add to wishlist
                var wishlistItem = new Wishlist
                {
                    UserId = userId,
                    ProductId = productId,
                    CreatedAt = DateTime.Now
                };

                _context.Add(wishlistItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int wishlistId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var wishlistItem = await _context.Set<Wishlist>()
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.UserId == userId);

            if (wishlistItem == null)
            {
                return NotFound();
            }

            _context.Remove(wishlistItem);
            await _context.SaveChangesAsync();

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

            var wishlistItem = await _context.Set<Wishlist>()
                .Include(w => w.Product)
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.UserId == userId);

            if (wishlistItem == null)
            {
                return NotFound();
            }

            // Check if item already exists in cart
            var cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == wishlistItem.ProductId);

            if (cartItem != null)
            {
                // Update existing cart item
                cartItem.Quantity += 1;
                cartItem.UpdatedAt = DateTime.Now;
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
            _context.Remove(wishlistItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
