using Serenity.ComponentModel;
using System;

namespace Admin_p.electroDB.Forms;

[FormScript("electroDB.ShoppingCarts")]
[BasedOnRow(typeof(ShoppingCartsRow), CheckNames = true)]
public class ShoppingCartsForm
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}