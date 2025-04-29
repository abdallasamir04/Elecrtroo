
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electro_ECommerce.Data;
using Electro_ECommerce.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Electro_ECommerce.Controllers
{
    public class CompareController : Controller
    {
        private readonly TechXpressDbContext _context;
        private const string CompareSessionKey = "CompareList";

        public CompareController(TechXpressDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var compareList = GetCompareListFromSession();

            if (compareList == null || !compareList.Any())
            {
                return View(new List<Product>());
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => compareList.Contains(p.ProductId))
                .ToListAsync();

            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCompare(int productId)
        {
            var compareList = GetCompareListFromSession() ?? new List<int>();

            // Check if product already exists in compare list
            if (!compareList.Contains(productId))
            {
                // Limit to 4 products for comparison
                if (compareList.Count >= 4)
                {
                    compareList.RemoveAt(0); // Remove the oldest item
                }

                compareList.Add(productId);
                SaveCompareListToSession(compareList);

                // If AJAX request, return JSON result
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Product added to compare list", count = compareList.Count });
                }

                TempData["SuccessMessage"] = "Product added to compare list.";
            }
            else
            {
                // If AJAX request, return JSON result
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Product already in compare list", count = compareList.Count });
                }

                TempData["InfoMessage"] = "Product is already in your compare list.";
            }

            // Redirect back to the referring page
            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
        }

        [HttpPost]
        public IActionResult RemoveFromCompare(int productId)
        {
            var compareList = GetCompareListFromSession();

            if (compareList != null && compareList.Contains(productId))
            {
                compareList.Remove(productId);
                SaveCompareListToSession(compareList);
                TempData["SuccessMessage"] = "Product removed from compare list.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearCompare()
        {
            HttpContext.Session.Remove(CompareSessionKey);
            TempData["SuccessMessage"] = "Compare list cleared.";
            return RedirectToAction(nameof(Index));
        }

        private List<int> GetCompareListFromSession()
        {
            var compareListJson = HttpContext.Session.GetString(CompareSessionKey);
            return string.IsNullOrEmpty(compareListJson)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(compareListJson);
        }

        private void SaveCompareListToSession(List<int> compareList)
        {
            var compareListJson = JsonSerializer.Serialize(compareList);
            HttpContext.Session.SetString(CompareSessionKey, compareListJson);
        }
    }
}
