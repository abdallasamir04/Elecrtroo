using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB.Columns;

[ColumnsScript("electroDB.Orders")]
[BasedOnRow(typeof(OrdersRow), CheckNames = true)]
public class OrdersColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int OrderId { get; set; }
    public string UserName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    [EditLink]
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}