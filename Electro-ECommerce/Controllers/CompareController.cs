using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electro_ECommerce.Models;
using Electro_ECommerce.ViewModels;
using Electro_ECommerce.Data;

namespace Electro_ECommerce.Controllers
{
    public class CompareController : Controller
    {
        private readonly TechXpressDbContext _context;

        public CompareController(TechXpressDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var compareIds = GetCompareListFromCookie();
            if (compareIds.Count == 0)
            {
                return View(new List<ProductViewModel>());
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => compareIds.Contains(p.ProductId))
                .ToListAsync();

            var productViewModels = products.Select(ProductViewModel.FromProduct).ToList();

            return View(productViewModels);
        }

        [HttpPost]
        public IActionResult AddToCompare(int productId)
        {
            var compareIds = GetCompareListFromCookie();

            // Only allow up to 4 products to compare
            if (!compareIds.Contains(productId) && compareIds.Count < 4)
            {
                compareIds.Add(productId);
                SaveCompareListToCookie(compareIds);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveFromCompare(int productId)
        {
            var compareIds = GetCompareListFromCookie();

            if (compareIds.Contains(productId))
            {
                compareIds.Remove(productId);
                SaveCompareListToCookie(compareIds);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearCompare()
        {
            Response.Cookies.Delete("CompareList");
            return RedirectToAction(nameof(Index));
        }

        private List<int> GetCompareListFromCookie()
        {
            var compareList = Request.Cookies["CompareList"];
            if (string.IsNullOrEmpty(compareList))
            {
                return new List<int>();
            }

            return compareList.Split(',')
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(int.Parse)
                .ToList();
        }

        private void SaveCompareListToCookie(List<int> compareIds)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                IsEssential = true
            };

            Response.Cookies.Append("CompareList", string.Join(",", compareIds), options);
        }
    }
}
