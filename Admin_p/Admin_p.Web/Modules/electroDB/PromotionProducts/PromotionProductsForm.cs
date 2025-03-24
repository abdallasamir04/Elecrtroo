using Serenity.ComponentModel;
using System;

namespace Admin_p.electroDB.Forms;

[FormScript("electroDB.PromotionProducts")]
[BasedOnRow(typeof(PromotionProductsRow), CheckNames = true)]
public class PromotionProductsForm
{
    public int PromotionId { get; set; }
    public int ProductId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}