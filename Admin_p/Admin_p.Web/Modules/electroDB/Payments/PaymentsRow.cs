using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB;

[ConnectionKey("Electro"), Module("electroDB"), TableName("Payments")]
[DisplayName("Payments"), InstanceName("Payments")]
[ReadPermission("Payments")]
[ModifyPermission("Payments")]
[ServiceLookupPermission("Payments")]
public sealed class PaymentsRow : Row<PaymentsRow.RowFields>, IIdRow, INameRow
{
    const string jOrder = nameof(jOrder);

    [DisplayName("Payment Id"), Column("PaymentID"), Identity, IdProperty]
    public int? PaymentId { get => fields.PaymentId[this]; set => fields.PaymentId[this] = value; }

    [DisplayName("Order"), Column("OrderID"), NotNull, ForeignKey(typeof(OrdersRow)), LeftJoin(jOrder), TextualField(nameof(OrderStatus))]
    [ServiceLookupEditor(typeof(OrdersRow))]
    public int? OrderId { get => fields.OrderId[this]; set => fields.OrderId[this] = value; }

    [DisplayName("Payment Method"), Size(50), NotNull, QuickSearch, NameProperty]
    public string PaymentMethod { get => fields.PaymentMethod[this]; set => fields.PaymentMethod[this] = value; }

    [DisplayName("Payment Status"), Size(50), NotNull]
    public string PaymentStatus { get => fields.PaymentStatus[this]; set => fields.PaymentStatus[this] = value; }

    [DisplayName("Transaction Date")]
    public DateTime? TransactionDate { get => fields.TransactionDate[this]; set => fields.TransactionDate[this] = value; }

    [DisplayName("Amount"), Size(18), Scale(2), NotNull]
    public decimal? Amount { get => fields.Amount[this]; set => fields.Amount[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    [DisplayName("Transaction Id"), Column("TransactionID"), Size(255)]
    public string TransactionId { get => fields.TransactionId[this]; set => fields.TransactionId[this] = value; }

    [DisplayName("Order Status"), Origin(jOrder, nameof(OrdersRow.Status))]
    public string OrderStatus { get => fields.OrderStatus[this]; set => fields.OrderStatus[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field PaymentId;
        public Int32Field OrderId;
        public StringField PaymentMethod;
        public StringField PaymentStatus;
        public DateTimeField TransactionDate;
        public DecimalField Amount;
        public DateTimeField CreatedAt;
        public DateTimeField UpdatedAt;
        public StringField TransactionId;

        public StringField OrderStatus;
    }
}