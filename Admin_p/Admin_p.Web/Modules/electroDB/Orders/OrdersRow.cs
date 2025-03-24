using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB;

[ConnectionKey("Electro"), Module("electroDB"), TableName("Orders")]
[DisplayName("Orders"), InstanceName("Orders")]
[ReadPermission("Orders")]
[ModifyPermission("Orders")]
[ServiceLookupPermission("Orders")]
public sealed class OrdersRow : Row<OrdersRow.RowFields>, IIdRow, INameRow
{
    const string jUser = nameof(jUser);

    [DisplayName("Order Id"), Column("OrderID"), Identity, IdProperty]
    public int? OrderId { get => fields.OrderId[this]; set => fields.OrderId[this] = value; }

    [DisplayName("User"), Column("UserID"), NotNull, ForeignKey(typeof(UsersRow)), LeftJoin(jUser), TextualField(nameof(UserName))]
    [ServiceLookupEditor(typeof(UsersRow))]
    public int? UserId { get => fields.UserId[this]; set => fields.UserId[this] = value; }

    [DisplayName("Order Date")]
    public DateTime? OrderDate { get => fields.OrderDate[this]; set => fields.OrderDate[this] = value; }

    [DisplayName("Total Amount"), Size(18), Scale(2), NotNull]
    public decimal? TotalAmount { get => fields.TotalAmount[this]; set => fields.TotalAmount[this] = value; }

    [DisplayName("Status"), Size(50), NotNull, QuickSearch, NameProperty]
    public string Status { get => fields.Status[this]; set => fields.Status[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    [DisplayName("User User Name"), Origin(jUser, nameof(UsersRow.UserName))]
    public string UserName { get => fields.UserName[this]; set => fields.UserName[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field OrderId;
        public Int32Field UserId;
        public DateTimeField OrderDate;
        public DecimalField TotalAmount;
        public StringField Status;
        public DateTimeField CreatedAt;
        public DateTimeField UpdatedAt;

        public StringField UserName;
    }
}