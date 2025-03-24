using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB.Columns;

[ColumnsScript("electroDB.Products")]
[BasedOnRow(typeof(ProductsRow), CheckNames = true)]
public class ProductsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int ProductId { get; set; }
    [EditLink]
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string CategoryName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ImagePath { get; set; }
}