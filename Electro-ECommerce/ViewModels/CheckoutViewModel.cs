using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Electro_ECommerce.Models;

namespace Electro_ECommerce.ViewModels
{
    public class CheckoutViewModel
    {
        // Customer Information
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; } = string.Empty;

        // Shipping Information
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; } = string.Empty;

        // Payment Information
        [Required(ErrorMessage = "Payment method is required")]
        public string PaymentMethod { get; set; } = string.Empty;

        // Credit Card Information (if applicable)
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public int? ExpMonth { get; set; }
        public int? ExpYear { get; set; }
        public string? Cvv { get; set; }

        // Order Summary
        public List<ShoppingCart> CartItems { get; set; } = new List<ShoppingCart>();
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }

        // Helper method to calculate totals
        public void CalculateTotals()
        {
            Subtotal = CartItems.Sum(item => item.Product.Price * item.Quantity);
            Tax = Subtotal * 0.1m; // 10% tax
            Shipping = CartItems.Any() ? 10.00m : 0; // $10 shipping fee if cart has items
            Total = Subtotal + Tax + Shipping;
        }
    }
}
