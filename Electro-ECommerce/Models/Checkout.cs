using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro_ECommerce.Models
{
    public class Checkout
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Payment method is required")]
        public string PaymentMethod { get; set; } = string.Empty;

        // Total from ShoppingCart
        public decimal OrderTotal { get; set; }

        // Additional properties needed for the checkout view
        public List<ShoppingCart> CartItems { get; set; } = new List<ShoppingCart>();
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
    }
}
