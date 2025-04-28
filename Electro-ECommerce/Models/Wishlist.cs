using System;
using System.Collections.Generic;

namespace Electro_ECommerce.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
