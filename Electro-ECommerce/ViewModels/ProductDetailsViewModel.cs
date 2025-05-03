using System;
using System.Collections.Generic;
using System.Linq;
using Electro_ECommerce.Models;

namespace Electro_ECommerce.ViewModels
{
    public class ProductDetailsViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public bool IsOnSale { get; set; }
        public decimal? OriginalPrice { get; set; }
        public bool IsInStock => StockQuantity > 0;
        public List<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
        public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
        public Dictionary<string, string>? Specifications { get; set; }
        public Dictionary<string, List<string>>? AvailableOptions { get; set; }
        public List<ProductViewModel> RelatedProducts { get; set; } = new List<ProductViewModel>();

        public class ReviewViewModel
        {
            public int ReviewId { get; set; }
            public int ProductId { get; set; }
            public string UserName { get; set; } = string.Empty;
            public int Rating { get; set; }
            public string? Comment { get; set; }
            public DateTime Date { get; set; }

          
        }

        public static ProductDetailsViewModel FromProduct(Product product, List<ProductViewModel> relatedProducts)
        {
            var viewModel = new ProductDetailsViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name,
                ImagePath = product.ImagePath,
                IsOnSale = product.DiscountPercentage > 0,
                OriginalPrice = product.DiscountPercentage > 0 ? product.Price * (1 + product.DiscountPercentage / 100) : null,
                RelatedProducts = relatedProducts,
                // Add some sample specifications (these would typically come from your database)
                Specifications = new Dictionary<string, string>
                {
                    { "Brand", "Electro" },
                    { "Model", "E-" + product.ProductId },
                    { "Warranty", "1 Year" },
                    { "Condition", "New" }
                },
                // Add some sample options (these would typically come from your database)
                AvailableOptions = new Dictionary<string, List<string>>
                {
                    { "Color", new List<string> { "Black", "White", "Silver" } },
                    { "Size", new List<string> { "Small", "Medium", "Large" } }
                }
            };

            // Map reviews if they exist
            if (product.Reviews != null)
            {
                viewModel.Reviews = product.Reviews.Select(r => new ReviewViewModel
                {
                    ReviewId = r.ReviewId,
                    ProductId = r.ProductId,
                    UserName = r.UserName,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Date = r.Date
                }).ToList();
            }

            return viewModel;
        }
    }
}
