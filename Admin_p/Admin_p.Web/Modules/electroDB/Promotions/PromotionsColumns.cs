using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB.Columns;

[ColumnsScript("electroDB.Promotions")]
[BasedOnRow(typeof(PromotionsRow), CheckNames = true)]
public class PromotionsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int PromotionId { get; set; }
    [EditLink]
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}