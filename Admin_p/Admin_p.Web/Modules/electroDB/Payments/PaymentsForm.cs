using Serenity.ComponentModel;
using System;

namespace Admin_p.electroDB.Forms;

[FormScript("electroDB.Payments")]
[BasedOnRow(typeof(PaymentsRow), CheckNames = true)]
public class PaymentsForm
{
    public int OrderId { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string TransactionId { get; set; }
}