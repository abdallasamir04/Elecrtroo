using Electro_ECommerce.Models;
using Electro_ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Electro_ECommerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> _orderRepository;

        public OrdersController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public ActionResult Index()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Add(order);
                _orderRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public ActionResult Edit(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.OrderId) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    order.UpdatedAt = DateTime.Now;
                    _orderRepository.Update(order);
                    _orderRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public ActionResult Delete(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) return NotFound();
            _orderRepository.Delete(order);
            _orderRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _orderRepository.Find(e => e.OrderId == id).Any();
        }
    }
}