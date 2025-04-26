using System.ComponentModel.DataAnnotations;

namespace Electro_ECommerce.Models

{
    public class Checkout
    {
        
        public string FullName { get; set; }

        
        public string Address { get; set; }

        
        public string City { get; set; }

        [Phone(ErrorMessage = "Please call a valid phone number.")]
        public string PhoneNumber { get; set; }

        
        
        public string PaymentMethod { get; set; }

        // Total from ShoppingCart
        public decimal OrderTotal { get; set; }

    }
}
