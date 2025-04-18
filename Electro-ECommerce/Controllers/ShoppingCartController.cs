using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electro_ECommerce.Data;
using Electro_ECommerce.Models;
using Microsoft.AspNetCore.Identity;

[Authorize]
public class ShoppingCartController : Controller
{
	private readonly TechXpressDbContext _context;
	private readonly UserManager<User> _userManager;

	public ShoppingCartController(TechXpressDbContext context, UserManager<User> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	[HttpPost]
	public async Task<IActionResult> AddToCart(int ProductId, int quantity = 1)
	{
		var user = await _userManager.GetUserAsync(User);
		if (user == null)
			return RedirectToAction("Login", "Account");

		var existingCartItem = await _context.ShoppingCarts
			.FirstOrDefaultAsync(c => c.UserId == user.Id && c.ProductId == ProductId);

		if (existingCartItem != null)
		{
			existingCartItem.Quantity += quantity;
			existingCartItem.UpdatedAt = DateTime.Now;
		}
		else
		{
			var cartItem = new ShoppingCart
			{
				UserId = user.Id,
				ProductId = ProductId,
				Quantity = quantity,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			};

			await _context.ShoppingCarts.AddAsync(cartItem);
		}

		await _context.SaveChangesAsync();

		return RedirectToAction("Cart", "ShoppingCart"); 
	}
	public async Task<IActionResult> Cart()
	{
		var user = await _userManager.GetUserAsync(User);
		if (user == null) return RedirectToAction("Login", "Account");

		var cartItems = await _context.ShoppingCarts
			.Include(c => c.Product)
			.Where(c => c.UserId == user.Id)
			.ToListAsync();

		return View(cartItems);
	}
	[HttpPost]
	public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
	{
		var item = await _context.ShoppingCarts.FindAsync(cartId);
		if (item != null && quantity > 0)
		{
			item.Quantity = quantity;
			item.UpdatedAt = DateTime.Now;
			await _context.SaveChangesAsync();
		}
		return RedirectToAction("Cart");
	}

	[HttpPost]
	public async Task<IActionResult> RemoveFromCart(int cartId)
	{
		var item = await _context.ShoppingCarts.FindAsync(cartId);
		if (item != null)
		{
			_context.ShoppingCarts.Remove(item);
			await _context.SaveChangesAsync();
		}
		return RedirectToAction("Cart");
	}

}
