using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro_ECommerce.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal DiscountPercentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        // Add these missing properties
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<PromotionProduct>? PromotionProducts { get; set; }
    }
}