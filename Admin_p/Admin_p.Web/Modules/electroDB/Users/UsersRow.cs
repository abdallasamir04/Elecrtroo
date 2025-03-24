using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB;

[ConnectionKey("Electro"), Module("electroDB"), TableName("Users")]
[DisplayName("Users"), InstanceName("Users")]
[ReadPermission("Users")]
[ModifyPermission("Users")]
[ServiceLookupPermission("Users")]
public sealed class UsersRow : Row<UsersRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("User Id"), Column("UserID"), Identity, IdProperty]
    public int? UserId { get => fields.UserId[this]; set => fields.UserId[this] = value; }

    [DisplayName("User Name"), Size(100), NotNull, QuickSearch, NameProperty]
    public string UserName { get => fields.UserName[this]; set => fields.UserName[this] = value; }

    [DisplayName("Email"), Size(100), NotNull]
    public string Email { get => fields.Email[this]; set => fields.Email[this] = value; }

    [DisplayName("Password Hash"), NotNull]
    public string PasswordHash { get => fields.PasswordHash[this]; set => fields.PasswordHash[this] = value; }

    [DisplayName("Role"), Size(50), NotNull]
    public string Role { get => fields.Role[this]; set => fields.Role[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    [DisplayName("Shipping Address")]
    public string ShippingAddress { get => fields.ShippingAddress[this]; set => fields.ShippingAddress[this] = value; }

    [DisplayName("Phone Number"), Size(20)]
    public string PhoneNumber { get => fields.PhoneNumber[this]; set => fields.PhoneNumber[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field UserId;
        public StringField UserName;
        public StringField Email;
        public StringField PasswordHash;
        public StringField Role;
        public DateTimeField CreatedAt;
        public DateTimeField UpdatedAt;
        public StringField ShippingAddress;
        public StringField PhoneNumber;

    }
}