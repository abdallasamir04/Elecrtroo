using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_ECommerce.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? ImagePath { get; set; }

        public int CategoryId { get; set; }

        [NotMapped]
        public string? CategoryName => Category?.Name;

        public int StockQuantity { get; set; }

        public bool IsOnSale { get; set; }

        public bool IsNew { get; set; }

        public bool IsFeatured { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; } = new List<PromotionProduct>();

    }
}
