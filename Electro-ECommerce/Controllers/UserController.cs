using Electro_ECommerce.Data;
using Electro_ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Electro_ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly TechXpressDbContext _context;

        public UserController(UserManager<User> userManager, TechXpressDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            // Set default values for statistics
            ViewBag.OrderCount = 0;
            ViewBag.TotalSpent = 0;

            // Try to get order statistics if available
            try
            {
                // Check if Orders table exists and has appropriate properties
                if (_context.Orders != null)
                {
                    // Count orders for this user
                    ViewBag.OrderCount = await _context.Orders
                        .CountAsync(o => o.UserId == id);

                    // Calculate total spent - adjust property names to match your Order model
                    // For example, if your Order model has TotalPrice instead of Total
                    ViewBag.TotalSpent = await _context.Orders
                        .Where(o => o.UserId == id && o.Status == "Completed")
                        .SumAsync(o => o.TotalAmount); // Change this to match your actual property name
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't crash
                Console.WriteLine($"Error fetching user statistics: {ex.Message}");
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string userName, string email, string password, string? role, string? shippingAddress, string? phoneNumber)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return View();

            var user = new User
            {
                UserName = userName,
                Email = email,
                Role = role,
                ShippingAddress = shippingAddress,
                PhoneNumber = phoneNumber,
                CreatedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User created successfully";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, User model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null)
                        return NotFound();

                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Role = model.Role;
                    user.ShippingAddress = model.ShippingAddress;
                    user.EmailConfirmed = model.EmailConfirmed;
                    user.TwoFactorEnabled = model.TwoFactorEnabled;
                    user.UpdatedAt = DateTime.Now;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "User updated successfully";
                        return RedirectToAction(nameof(Details), new { id = user.Id });
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            return View(model);
        }

        // POST: User/ToggleStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id, bool activate)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "User ID not provided";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Update the EmailConfirmed property based on the activate parameter
                user.EmailConfirmed = activate;
                user.UpdatedAt = DateTime.Now;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = activate
                        ? $"User {user.UserName} has been activated"
                        : $"User {user.UserName} has been suspended";
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    TempData["ErrorMessage"] = $"Failed to update user status: {errors}";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction(nameof(Details), new { id = user.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "User ID not provided";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found";
                return RedirectToAction(nameof(Index));
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"User {user.UserName} deleted successfully";
                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            TempData["ErrorMessage"] = $"Failed to delete user: {errors}";
            return RedirectToAction(nameof(Index));
        }

        // For AJAX responses
        private IActionResult JsonResponse(bool success, string message)
        {
            return Json(new { success, message });
        }
    }
}