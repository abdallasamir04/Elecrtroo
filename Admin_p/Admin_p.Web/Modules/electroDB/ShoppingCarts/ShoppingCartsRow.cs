using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB;

[ConnectionKey("Electro"), Module("electroDB"), TableName("ShoppingCarts")]
[DisplayName("Shopping Carts"), InstanceName("Shopping Carts")]
[ReadPermission("ShoppingCarts")]
[ModifyPermission("ShoppingCarts")]
public sealed class ShoppingCartsRow : Row<ShoppingCartsRow.RowFields>, IIdRow
{
    const string jUser = nameof(jUser);
    const string jProduct = nameof(jProduct);

    [DisplayName("Cart Id"), Column("CartID"), Identity, IdProperty]
    public int? CartId { get => fields.CartId[this]; set => fields.CartId[this] = value; }

    [DisplayName("User"), Column("UserID"), NotNull, ForeignKey(typeof(UsersRow)), LeftJoin(jUser), TextualField(nameof(UserName))]
    [ServiceLookupEditor(typeof(UsersRow))]
    public int? UserId { get => fields.UserId[this]; set => fields.UserId[this] = value; }

    [DisplayName("Product"), Column("ProductID"), NotNull, ForeignKey(typeof(ProductsRow)), LeftJoin(jProduct)]
    [TextualField(nameof(ProductName)), ServiceLookupEditor(typeof(ProductsRow))]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }

    [DisplayName("Quantity"), NotNull]
    public int? Quantity { get => fields.Quantity[this]; set => fields.Quantity[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    [DisplayName("User User Name"), Origin(jUser, nameof(UsersRow.UserName))]
    public string UserName { get => fields.UserName[this]; set => fields.UserName[this] = value; }

    [DisplayName("Product Name"), Origin(jProduct, nameof(ProductsRow.Name))]
    public string ProductName { get => fields.ProductName[this]; set => fields.ProductName[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field CartId;
        public Int32Field UserId;
        public Int32Field ProductId;
        public Int32Field Quantity;
        public DateTimeField CreatedAt;
        public DateTimeField UpdatedAt;

        public StringField UserName;
        public StringField ProductName;
    }
}