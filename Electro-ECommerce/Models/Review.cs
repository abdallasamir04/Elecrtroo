using System;

namespace Electro_ECommerce.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }  // Foreign key to Product
    }
}
