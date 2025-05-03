using Electro_ECommerce.Models;
using Electro_ECommerce.Repositories;
using Electro_ECommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Electro_ECommerce.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }
        public IActionResult Index()
        {
            var products = _productRepository.GetAll().ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            // Log the request for debugging
            System.Diagnostics.Debug.WriteLine($"Requesting product details for ID: {id}");

            // Get the product with its related data
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                System.Diagnostics.Debug.WriteLine($"Product with ID {id} not found");
                return NotFound();
            }

            System.Diagnostics.Debug.WriteLine($"Found product: {product.Name}");

            // Get related products (products in the same category)
            var relatedProducts = new List<ProductViewModel>();

            if (product.CategoryId.HasValue)
            {
                System.Diagnostics.Debug.WriteLine($"Finding related products for category ID: {product.CategoryId}");

                var relatedProductsData = _productRepository.Find(p =>
                    p.CategoryId == product.CategoryId &&
                    p.ProductId != product.ProductId)
                    .Take(4)
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"Found {relatedProductsData.Count} related products");

                relatedProducts = relatedProductsData.Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    ImagePath = p.ImagePath,
                    CategoryName = p.Category?.Name,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    // Handle DiscountPercentage if it exists
                    DiscountPercentage = p.DiscountPercentage,
                    IsOnSale = p.DiscountPercentage > 0,
                    OriginalPrice = p.DiscountPercentage > 0 ? p.Price * (1 + p.DiscountPercentage / 100) : null
                }).ToList();
            }

            // Convert the product to a ProductDetailsViewModel
            var viewModel = ProductDetailsViewModel.FromProduct(product, relatedProducts);

            System.Diagnostics.Debug.WriteLine("ViewModel created successfully");

            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAll().ToList();
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _productRepository.Add(product);
        //        _productRepository.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.Categories = _categoryRepository.GetAll().ToList();
        //    return View(product);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? ProductImage = null)
        {
            if (ModelState.IsValid)
            {
                if (ProductImage != null && ProductImage.Length > 0)
                {
                    // تحديد المسار داخل wwwroot/images
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ProductImage.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // التأكد من أن المجلد موجود، وإذا لم يكن كذلك يتم إنشاؤه
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // حفظ الصورة في المسار المحدد
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProductImage.CopyToAsync(stream);
                    }

                    // حفظ المسار في قاعدة البيانات
                    product.ImagePath = "/images/" + uniqueFileName;
                }

                _productRepository.Add(product);
                _productRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _categoryRepository.GetAll().ToList();
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product, IFormFile ImageFile)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            // Get the existing product to preserve data that might not be in the form
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update the existing product with form values
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.UpdatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle image upload if a new image was provided
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        // Save the image and get the path
                        string imagePath = SaveProductImage(ImageFile, product.ProductId);

                        // Update the product's image path
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            existingProduct.ImagePath = imagePath;
                        }
                    }

                    // Update the product in the database
                    _productRepository.Update(existingProduct);
                    // Replace this line:

    
                    _productRepository.SaveChanges();

                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the error
                    ModelState.AddModelError("", $"Error updating product: {ex.Message}");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.Categories = _categoryRepository.GetAll().ToList();
            ViewBag.ProductImage = existingProduct.ImagePath; // Preserve the current image
            return View(product);
        }

        // Helper method to save product image - improved version
        private string SaveProductImage(IFormFile imageFile, int productId)
        {
            try
            {
                // Create directory if it doesn't exist
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "products");
                Directory.CreateDirectory(uploadsFolder); // This will create if it doesn't exist

                // Generate unique filename
                string uniqueFileName = $"product_{productId}_{DateTime.Now.Ticks}{Path.GetExtension(imageFile.FileName)}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                // Return the relative path for storing in the database
                return $"/img/products/{uniqueFileName}";
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error saving image: {ex.Message}");
                return null;
            }
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _categoryRepository.GetAll().ToList();

            // Add the current product image to ViewBag
            ViewBag.ProductImage = product.ImagePath; // Use the Image property directly

            return View(product);
        }


        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Delete(product);
            _productRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _productRepository.Find(e => e.ProductId == id).Any();
        }
    }
}