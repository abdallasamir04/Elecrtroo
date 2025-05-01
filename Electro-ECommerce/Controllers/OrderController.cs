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

    // GET: /Order/Cancel/5
    [HttpGet]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.OrderId == id && o.UserId == userId);

        if (order == null)
            return NotFound();

        // Optional: Prevent cancelling if already cancelled or shipped
        if (order.Status == "Cancelled" || order.Status == "Shipped")
        {
            return RedirectToAction("Details", new { id });
        }

        return View(order); // Return Cancel.cshtml view
    }

    // POST: /Order/ConfirmCancel
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmCancel(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.OrderId == id && o.UserId == userId);

        if (order == null)
            return NotFound();

        if (order.Status == "Cancelled" || order.Status == "Shipped")
        {
            return RedirectToAction("Details", new { id });
        }

        order.Status = "Cancelled";
        order.UpdatedAt = DateTime.Now;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id });
    }

    // POST: /Order/PlaceOrder
    public async Task<IActionResult> PlaceOrder()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = await _context.ShoppingCarts
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        if (!cartItems.Any())
        {
            return RedirectToAction("Index", "Home");
        }

        decimal totalAmount = 0;
        foreach (var item in cartItems)
        {
            if (item.Product == null) continue;
            totalAmount += item.Product.Price * item.Quantity;
        }

        if (totalAmount == 0)
        {
            return RedirectToAction("Index", "Home");
        }

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalAmount = totalAmount,
            Status = "Pending",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        foreach (var cartItem in cartItems)
        {
            if (cartItem.Product == null) continue;

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

        _context.ShoppingCarts.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Order");
    }
}
