using System;
using System.ComponentModel.DataAnnotations;

namespace Electro_ECommerce.Models
{
    public class PromotionProduct
    {
        public int PromotionProductId { get; set; }

        public int ProductId { get; set; }

        public int PromotionId { get; set; }

        // Add these missing properties
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public virtual Product? Product { get; set; }
        public virtual Promotion? Promotion { get; set; }
    }
}