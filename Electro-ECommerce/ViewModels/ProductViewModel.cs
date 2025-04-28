using Electro_ECommerce.Models;

namespace Electro_ECommerce.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal? OriginalPrice { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? ImagePath { get; set; }

        public bool IsNew { get; set; }

        public bool IsOnSale { get; set; }

        public static ProductViewModel FromProduct(Product product)
        {
            return new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                OriginalPrice = product.DiscountPercentage > 0 ? product.Price * (1 + product.DiscountPercentage / 100) : null,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "Uncategorized",
                ImagePath = product.ImagePath,
                IsNew = (DateTime.Now - product.CreatedAt).TotalDays <= 30,
                IsOnSale = product.DiscountPercentage > 0
            };
        }

        
    }
}