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
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly TechXpressDbContext _context;
        private readonly UserManager<User> _userManager;

        public CheckoutController(TechXpressDbContext context, UserManager<User> userManager)
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

            var cartItems = await _context.ShoppingCarts
                .Include(c => c.Product)
                .ThenInclude(p => p.Category)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return RedirectToAction("Cart", "ShoppingCart");
            }

            var user = await _userManager.FindByIdAsync(userId);

            // Calculate totals
            decimal subtotal = cartItems.Sum(item => item.Product.Price * item.Quantity);
            decimal tax = subtotal * 0.1m; // 10% tax
            decimal shipping = 10.00m; // Fixed shipping cost
            decimal total = subtotal + tax + shipping;

            var checkout = new Checkout
            {
                CartItems = cartItems,
                Subtotal = subtotal,
                Tax = tax,
                Shipping = shipping,
                Total = total,
                PhoneNumber = user?.PhoneNumber ?? string.Empty,
                Address = user?.ShippingAddress ?? string.Empty
            };

            return View(checkout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessCheckout(Checkout model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cartItems = await _context.ShoppingCarts
                    .Include(c => c.Product)
                    .ThenInclude(p => p.Category)
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                // Recalculate totals
                decimal subtotal = cartItems.Sum(item => item.Product.Price * item.Quantity);
                decimal tax = subtotal * 0.1m; // 10% tax
                decimal shipping = 10.00m; // Fixed shipping cost
                decimal total = subtotal + tax + shipping;

                model.CartItems = cartItems;
                model.Subtotal = subtotal;
                model.Tax = tax;
                model.Shipping = shipping;
                model.Total = total;

                return View("Index", model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get cart items
            var items = await _context.ShoppingCarts
                .Include(c => c.Product)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            if (!items.Any())
            {
                return RedirectToAction("Cart", "ShoppingCart");
            }

            // Calculate totals
            decimal orderSubtotal = items.Sum(item => item.Product.Price * item.Quantity);
            decimal orderTax = orderSubtotal * 0.1m; // 10% tax
            decimal orderShipping = 10.00m; // Fixed shipping cost
            decimal orderTotal = orderSubtotal + orderTax + orderShipping;

            // Create order
            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.Now,
                TotalAmount = orderTotal,
                Status = "Pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Create order details
            foreach (var item in items)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                    Subtotal = item.Product.Price * item.Quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.OrderDetails.Add(orderDetail);
            }

            // Create payment record
            var payment = new Payment
            {
                OrderId = order.OrderId,
                PaymentMethod = model.PaymentMethod,
                PaymentStatus = "Pending",
                TransactionDate = DateTime.Now,
                Amount = orderTotal,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TransactionId = Guid.NewGuid().ToString() // Generate a unique transaction ID
            };

            _context.Payments.Add(payment);

            // Update user shipping address if it has changed
            if (!string.IsNullOrEmpty(model.Address) && model.Address != user.ShippingAddress)
            {
                user.ShippingAddress = model.Address;
                await _userManager.UpdateAsync(user);
            }

            // Clear the cart
            _context.ShoppingCarts.RemoveRange(items);

            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var userId = _userManager.GetUserId(User);
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
