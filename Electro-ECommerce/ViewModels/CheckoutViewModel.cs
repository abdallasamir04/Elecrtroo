using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Electro_ECommerce.Data;
using Electro_ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Electro_ECommerce.ViewModels; // Add this using directive at the top of the file

[Authorize]
public class CheckoutController : Controller
{
    private readonly TechXpressDbContext _context;

    public CheckoutController(TechXpressDbContext context)
    {
        _context = context;
    }

    // GET: Checkout/Index
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Get the items in the user's cart
        var cartItems = await _context.ShoppingCarts
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        if (!cartItems.Any())
        {
            return RedirectToAction("Index", "ShoppingCart"); // Redirect to the ShoppingCart if the cart is empty
        }

        // Calculate totals
        decimal subtotal = cartItems.Sum(item => item.Product.Price * item.Quantity);
        decimal tax = subtotal * 0.1m; // 10% tax
        decimal shipping = 10.00m; // Fixed shipping cost
        decimal total = subtotal + tax + shipping;

        // Prepare the checkout view model
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

    // POST: Checkout/PlaceOrder
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessCheckout(Checkout model)
    {
        if (!ModelState.IsValid)
        {
            // Re-populate cart items if model is invalid
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _context.ShoppingCarts
                .Where(c => c.UserId == currentUserId)
                .Include(c => c.Product)
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

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Get cart items
        var items = await _context.ShoppingCarts
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        if (!items.Any())
        {
            return RedirectToAction("Index", "ShoppingCart");
        }

        // Calculate totals for the order
        decimal orderSubtotal = items.Sum(item => item.Product.Price * item.Quantity);
        decimal orderTax = orderSubtotal * 0.1m;
        decimal orderShipping = 10.00m;
        decimal orderTotal = orderSubtotal + orderTax + orderShipping;

        // Create the Order
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

        // Add order details
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

        // Create a payment record (mock for now)
        var payment = new Payment
        {
            OrderId = order.OrderId,
            PaymentMethod = model.PaymentMethod,
            PaymentStatus = "Pending",
            TransactionDate = DateTime.Now,
            Amount = orderTotal,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.Payments.Add(payment);

        // Remove cart items after placing the order
        _context.ShoppingCarts.RemoveRange(items);

        await _context.SaveChangesAsync();

        // Redirect to the 'My Orders' page (OrderController Index)
        return RedirectToAction("Index", "Order"); // Redirecting to Order Controller's Index action to view the orders
    }

    // GET: Checkout/OrderConfirmation/5
    public async Task<IActionResult> OrderConfirmation(int orderId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var order = await _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
}