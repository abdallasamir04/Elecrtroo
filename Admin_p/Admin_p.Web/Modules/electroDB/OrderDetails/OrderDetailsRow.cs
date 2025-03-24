using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB;

[ConnectionKey("Electro"), Module("electroDB"), TableName("OrderDetails")]
[DisplayName("Order Details"), InstanceName("Order Details")]
[ReadPermission("OrderDetails")]
[ModifyPermission("OrderDetails")]
public sealed class OrderDetailsRow : Row<OrderDetailsRow.RowFields>, IIdRow
{
    const string jOrder = nameof(jOrder);
    const string jProduct = nameof(jProduct);

    [DisplayName("Order Detail Id"), Column("OrderDetailID"), Identity, IdProperty]
    public int? OrderDetailId { get => fields.OrderDetailId[this]; set => fields.OrderDetailId[this] = value; }

    [DisplayName("Order"), Column("OrderID"), NotNull, ForeignKey(typeof(OrdersRow)), LeftJoin(jOrder), TextualField(nameof(OrderStatus))]
    [ServiceLookupEditor(typeof(OrdersRow))]
    public int? OrderId { get => fields.OrderId[this]; set => fields.OrderId[this] = value; }

    [DisplayName("Product"), Column("ProductID"), NotNull, ForeignKey(typeof(ProductsRow)), LeftJoin(jProduct)]
    [TextualField(nameof(ProductName)), ServiceLookupEditor(typeof(ProductsRow))]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }

    [DisplayName("Quantity"), NotNull]
    public int? Quantity { get => fields.Quantity[this]; set => fields.Quantity[this] = value; }

    [DisplayName("Unit Price"), Size(18), Scale(2), NotNull]
    public decimal? UnitPrice { get => fields.UnitPrice[this]; set => fields.UnitPrice[this] = value; }

    [DisplayName("Subtotal"), Size(18), Scale(2), NotNull]
    public decimal? Subtotal { get => fields.Subtotal[this]; set => fields.Subtotal[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    [DisplayName("Order Status"), Origin(jOrder, nameof(OrdersRow.Status))]
    public string OrderStatus { get => fields.OrderStatus[this]; set => fields.OrderStatus[this] = value; }

    [DisplayName("Product Name"), Origin(jProduct, nameof(ProductsRow.Name))]
    public string ProductName { get => fields.ProductName[this]; set => fields.ProductName[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field OrderDetailId;
        public Int32Field OrderId;
        public Int32Field ProductId;
        public Int32Field Quantity;
        public DecimalField UnitPrice;
        public DecimalField Subtotal;
        public DateTimeField CreatedAt;
        public DateTimeField UpdatedAt;

        public StringField OrderStatus;
        public StringField ProductName;
    }
}