using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB.Columns;

[ColumnsScript("electroDB.ShoppingCarts")]
[BasedOnRow(typeof(ShoppingCartsRow), CheckNames = true)]
public class ShoppingCartsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int CartId { get; set; }
    public string UserName { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}