using Electro_ECommerce.Models;

using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro_ECommerce.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Add this missing property
        public decimal DiscountPercentage { get; set; }

        // Navigation properties
        public Category? Category { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }  // <-- Add this

        // Collection navigation properties
        public virtual ICollection<ShoppingCart>? ShoppingCarts { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<PromotionProduct>? PromotionProducts { get; set; }
    }
}
