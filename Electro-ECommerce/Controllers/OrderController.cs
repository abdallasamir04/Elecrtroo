using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Electro_ECommerce.Data;
using Electro_ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class OrderController : Controller
{
    private readonly TechXpressDbContext _context;

    public OrderController(TechXpressDbContext context)
    {
        _context = context;
    }

    // GET: /Order/MyOrders
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orders = await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToListAsync();

        return View(orders);
    }


    // GET: /Order/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var order = await _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(o => o.OrderId == id && o.UserId == userId);

        if (order == null)
            return NotFound();

        return View(order);
    }

    public async Task<IActionResult> PlaceOrder()
    {
        // Get the current logged-in user
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Get the items in the user's cart, including the Product data
        var cartItems = await _context.ShoppingCarts
            .Where(c => c.UserId == userId)
            .Include(c => c.Product) // Ensure Product is loaded
            .ToListAsync();

        if (!cartItems.Any())
        {
            return RedirectToAction("Index", "Home"); // Redirect to the home page if the cart is empty
        }

        // Calculate the total amount for the order
        decimal totalAmount = 0;

        // Check if each cart item has a valid Product object
        foreach (var item in cartItems)
        {
            if (item.Product == null) // Check if Product is null
            {
                // You can log or handle the case where the product is missing
                // Optionally, you can also remove this cart item if the Product is missing
                continue; // Skip this item if Product is null
            }

            // If Product is valid, calculate totalAmount
            totalAmount += item.Product.Price * item.Quantity;
        }

        // If totalAmount is 0 (because all products in the cart were invalid), return an error or redirect
        if (totalAmount == 0)
        {
            return RedirectToAction("Index", "Home"); // Or return a custom error message
        }

        // Create a new Order
        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalAmount = totalAmount,
            Status = "Pending", // Set an initial order status (e.g., "Pending", "Paid", etc.)
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Add each cart item as OrderDetail
        foreach (var cartItem in cartItems)
        {
            if (cartItem.Product == null) continue; // Skip if no product is associated with the cart item

            var orderDetail = new OrderDetail
            {
                OrderId = order.OrderId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.Product.Price,
                Subtotal = cartItem.Product.Price * cartItem.Quantity,
                CreatedAt = DateTime.Now
            };

            _context.OrderDetails.Add(orderDetail);
        }

        // Remove cart items after the order is placed
        _context.ShoppingCarts.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        // Redirect to the 'My Orders' page
        return RedirectToAction("Index", "Order");
    }




}
