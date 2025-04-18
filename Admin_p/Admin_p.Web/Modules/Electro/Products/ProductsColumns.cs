using Serenity.ComponentModel;
using System;
using System.ComponentModel;
using Serenity.ComponentModel;


namespace Admin_p.Electro.Columns;

[ColumnsScript("Electro.Products")]
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


    // [ImageFormatter(FilenameFormat = "upload/Products/~", ScaleWidth = 100, ScaleHeight = 100)]
    
   // public string ImagePath { get; set; }
}