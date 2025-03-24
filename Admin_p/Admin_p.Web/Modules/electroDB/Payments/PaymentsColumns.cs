using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB.Columns;

[ColumnsScript("electroDB.Payments")]
[BasedOnRow(typeof(PaymentsRow), CheckNames = true)]
public class PaymentsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int PaymentId { get; set; }
    public string OrderStatus { get; set; }
    [EditLink]
    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string TransactionId { get; set; }
}