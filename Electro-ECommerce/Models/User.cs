﻿using Microsoft.AspNetCore.Identity;

namespace Electro_ECommerce.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Role { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
