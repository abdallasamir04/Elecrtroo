using Electro_ECommerce.Models;
using Electro_ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Electro_ECommerce.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly IRepository<Category> _categoryRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public CategoriesController(IRepository<Category> categoryRepository, IWebHostEnvironment webHostEnvironment)
		{
			_categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
			_webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
		}

		// GET: CategoriesController
		public ActionResult Index()
		{
			var categories = _categoryRepository.GetAll();
			return View(categories);
		}

		// GET: CategoriesController/Details/5
		public ActionResult Details(int id)
		{
			var categories = _categoryRepository.GetById(id);
			if (categories == null)
				return NotFound();

			return View(categories);
		}

		// GET: CategoriesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CategoriesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category category, IFormFile ImageFile)
		{
			if (ModelState.IsValid)
			{
				if (ImageFile != null && ImageFile.Length > 0)
				{
					string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
					string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
					string filePath = Path.Combine(uploadsFolder, uniqueFileName);

					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await ImageFile.CopyToAsync(fileStream);
					}


					category.ImageUrl = "/img/" + uniqueFileName;
				}

				_categoryRepository.Add(category);
				_categoryRepository.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}


		// GET: CategoriesController/Edit/5
		public ActionResult Edit(int id)
		{
			var category = _categoryRepository.GetById(id);
			if (category == null)
				return NotFound();

			return View(category);
		}

		// POST: CategoriesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Category category, IFormFile? ImageFile)
		{

			if ((id != category.CategoryId)) return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{

					if (ImageFile != null && ImageFile.Length > 0)
					{
						string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
						string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
						string filePath = Path.Combine(uploadsFolder, uniqueFileName);

						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							await ImageFile.CopyToAsync(fileStream);
						}

						category.ImageUrl = "/img/" + uniqueFileName;
					}
					category.UpdatedAt = DateTime.Now;
					_categoryRepository.Update(category);
					_categoryRepository.SaveChanges();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CategoryExists(category.CategoryId))
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
			return View(category);
		}

		// GET: CategoriesController/Delete/5
		public ActionResult Delete(int id)
		{
			var category = _categoryRepository.GetById(id);
			if (category == null)
				return NotFound();

			return View(category);

		}

		// POST: CategoriesController/Delete/5
		[HttpPost, ActionName("delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			var category = _categoryRepository.GetById(id);
			if (category == null) return NotFound();
			_categoryRepository.Delete(category);
			_categoryRepository.SaveChanges();
			return RedirectToAction(nameof(Index));

		}
		private bool CategoryExists(int id)
		{
			return _categoryRepository.Find(e => e.CategoryId == id).Any();
		}
		internal RedirectToActionResult Create(Category newCategory)
		{
			throw new NotImplementedException();
		}
	}
}