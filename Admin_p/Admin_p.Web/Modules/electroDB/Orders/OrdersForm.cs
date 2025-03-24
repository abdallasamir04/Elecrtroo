using Serenity.ComponentModel;
using System;

namespace Admin_p.electroDB.Forms;

[FormScript("electroDB.Orders")]
[BasedOnRow(typeof(OrdersRow), CheckNames = true)]
public class OrdersForm
{
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}