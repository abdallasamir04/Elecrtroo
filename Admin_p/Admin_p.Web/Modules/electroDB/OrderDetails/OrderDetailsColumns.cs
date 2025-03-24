using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB.Columns;

[ColumnsScript("electroDB.OrderDetails")]
[BasedOnRow(typeof(OrderDetailsRow), CheckNames = true)]
public class OrderDetailsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int OrderDetailId { get; set; }
    public string OrderStatus { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}