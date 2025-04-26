using Microsoft.AspNetCore.Mvc;
using Electro_ECommerce.Models;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Electro_ECommerce.Data;

namespace Electro_ECommerce.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly TechXpressDbContext _context;

        public CheckoutController(IConfiguration configuration, TechXpressDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToList();

            decimal total = cartItems.Sum(item => item.Product.Price * item.Quantity);

            var checkoutModel = new Checkout
            {
                OrderTotal = total
            };

            return View(checkoutModel);
        }

        [HttpPost]
        public IActionResult Index(Checkout model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToList();

            decimal total = cartItems.Sum(item => item.Product.Price * item.Quantity);

            var domain = "https://localhost:7243"; 

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = cartItems.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = item.Product.Price * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        }
                    },
                    Quantity = item.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + "/Checkout/Success",
                CancelUrl = domain + "/Checkout/Cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
