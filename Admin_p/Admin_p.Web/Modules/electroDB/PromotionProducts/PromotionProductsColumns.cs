using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB.Columns;

[ColumnsScript("electroDB.PromotionProducts")]
[BasedOnRow(typeof(PromotionProductsRow), CheckNames = true)]
public class PromotionProductsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int PromotionProductId { get; set; }
    public string PromotionName { get; set; }
    public string ProductName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}