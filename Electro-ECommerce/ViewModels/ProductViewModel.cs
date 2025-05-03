using System;
using System.Collections.Generic;
using Electro_ECommerce.Models;

namespace Electro_ECommerce.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageUrl => ImagePath; // Added property for compatibility
        public bool IsOnSale { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool IsNew { get; set; }
        public bool IsInStock => StockQuantity > 0; // Added property

        // Static method to convert from Product model to ProductViewModel
        public static ProductViewModel FromProduct(Product product)
        {
            return new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                ImagePath = product.ImagePath,
                IsOnSale = product.DiscountPercentage > 0,
                OriginalPrice = product.DiscountPercentage > 0 ? product.Price * (1 + product.DiscountPercentage / 100) : null,
                DiscountPercentage = product.DiscountPercentage,
                IsNew = (DateTime.Now - product.CreatedAt).TotalDays <= 30.0 // Use double for comparison
            };
        }
    }
}
