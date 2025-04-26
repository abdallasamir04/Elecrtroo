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

}
