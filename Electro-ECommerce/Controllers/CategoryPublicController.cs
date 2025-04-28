using Electro_ECommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_ECommerce.Controllers
{
    public class CategoryPublicController : Controller
    {
        private readonly TechXpressDbContext _context;

        public CategoryPublicController(TechXpressDbContext context)
        {
            _context = context;
        }

        [Route("CategoryPublic/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Name == name);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}
