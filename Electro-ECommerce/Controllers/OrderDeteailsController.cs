
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electro_ECommerce.Models;
using Electro_ECommerce.Data;

namespace Electro_ECommerce.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly TechXpressDbContext _context;

        public OrderDetailsController(TechXpressDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var orderDetails = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(await orderDetails.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);

            if (orderDetail == null)
                return NotFound();

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,Quantity,UnitPrice,Subtotal")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.CreatedAt = DateTime.Now;
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
                return NotFound();

            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,OrderId,ProductId,Quantity,UnitPrice,Subtotal")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    orderDetail.UpdatedAt = DateTime.Now;
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);

            if (orderDetail == null)
                return NotFound();

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderDetailId == id);
        }
    }
}