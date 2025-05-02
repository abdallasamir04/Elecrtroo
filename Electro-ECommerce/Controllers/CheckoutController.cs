using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Electro_ECommerce.Data;
using Electro_ECommerce.Models;
using Electro_ECommerce.ViewModels;
using Stripe.Checkout;

namespace Electro_ECommerce.Controllers
{
    [Authorize]
    [Route("Checkout")]
    public class CheckoutController : Controller
    {
        private readonly TechXpressDbContext _context;

        public CheckoutController(TechXpressDbContext context)
        {
            _context = context;
        }

        // GET: Checkout/Index
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any())
                return RedirectToAction("Cart", "ShoppingCart");

            decimal subtotal = cartItems.Sum(item => item.Product.Price * item.Quantity);
            decimal tax = subtotal * 0.1m;
            decimal shipping = 10.00m;
            decimal total = subtotal + tax + shipping;

            var checkoutViewModel = new Checkout
            {
                CartItems = cartItems,
                Subtotal = subtotal,
                Tax = tax,
                Shipping = shipping,
                Total = total
            };

            return View(checkoutViewModel);
        }

        // POST: Checkout/ProcessCheckout
        [HttpPost("ProcessCheckout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessCheckout(Checkout model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!items.Any())
                return RedirectToAction("Cart", "ShoppingCart");

            decimal orderSubtotal = items.Sum(item => item.Product.Price * item.Quantity);
            decimal orderTax = orderSubtotal * 0.1m;
            decimal orderShipping = 10.00m;
            decimal orderTotal = orderSubtotal + orderTax + orderShipping;

            // Step 1: Create a new order (still pending)
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = orderTotal,
                Status = "Pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                    Subtotal = item.Product.Price * item.Quantity,
                    CreatedAt = DateTime.Now
                };
                _context.OrderDetails.Add(orderDetail);
            }

            await _context.SaveChangesAsync();

            // Step 2: Redirect to Stripe Checkout
            var domain = "https://localhost:7243"; // Replace with your live domain

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = items.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        }
                    },
                    Quantity = item.Quantity
                }).ToList(),
                Mode = "payment",
                SuccessUrl = $"{domain}/Checkout/PaymentSuccess?orderId={order.OrderId}",
                CancelUrl = $"{domain}/Checkout/Index"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303); // Redirect to Stripe Checkout
        }

        // GET: Checkout/PaymentSuccess?orderId=123
        [HttpGet("PaymentSuccess")]
        public async Task<IActionResult> PaymentSuccess(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return NotFound();

            // Create Payment record after Stripe success
            var payment = new Payment
            {
                OrderId = order.OrderId,
                PaymentMethod = "Stripe",
                PaymentStatus = "Completed",
                TransactionDate = DateTime.Now,
                Amount = order.TotalAmount,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TransactionId = Guid.NewGuid().ToString() // In real app, fetch from Stripe webhook
            };

            _context.Payments.Add(payment);

            // Clear cart after successful payment
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = _context.ShoppingCarts.Where(c => c.UserId == userId);
            _context.ShoppingCarts.RemoveRange(cartItems);

            // Update order status
            order.Status = "Confirmed";
            order.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        // GET: Checkout/OrderConfirmation/5
        [HttpGet("OrderConfirmation/{orderId}")]
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
