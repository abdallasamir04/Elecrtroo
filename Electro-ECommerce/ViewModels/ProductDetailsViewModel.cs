using Electro_ECommerce.Models;
using System;
using System.Collections.Generic;

namespace Electro_ECommerce.ViewModels
{
    public class ProductDetailsViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public bool IsNew { get; set; }
        public bool IsOnSale { get; set; }
        public bool IsInStock => StockQuantity > 0;

        public Dictionary<string, List<string>> AvailableOptions { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, string> Specifications { get; set; } = new Dictionary<string, string>();

        public List<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
        public List<ProductViewModel> RelatedProducts { get; set; } = new List<ProductViewModel>();

        public static ProductDetailsViewModel FromProduct(Product product, List<ProductViewModel> relatedProducts)
        {
            return new ProductDetailsViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                OriginalPrice = product.DiscountPercentage > 0 ? product.Price * (1 + product.DiscountPercentage / 100) : null,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name ?? "Uncategorized",
                ImagePath = product.ImagePath,
                IsNew = (DateTime.Now - product.CreatedAt).TotalDays <= 30,
                IsOnSale = product.DiscountPercentage > 0,
                RelatedProducts = relatedProducts
            };
        }

        public class ReviewViewModel
        {
            public string UserName { get; set; }
            public int Rating { get; set; }
            public string Comment { get; set; }
            public DateTime Date { get; set; }

            // Add ProductId property to store the ID of the related product
            public int ProductId { get; set; }
        }

    }
}
