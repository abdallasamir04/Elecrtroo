using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.Identity;

[ConnectionKey("Electro"), Module("Identity"), TableName("AspNetUsers")]
[DisplayName("Asp Net Users"), InstanceName("Asp Net Users")]
[ReadPermission("Identity:User")]
[ModifyPermission("Identity:User")]
[ServiceLookupPermission("Identity:User")]
public sealed class AspNetUsersRow : Row<AspNetUsersRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Size(450), PrimaryKey, NotNull, IdProperty, QuickSearch, NameProperty]
    public string Id { get => fields.Id[this]; set => fields.Id[this] = value; }

    [DisplayName("First Name")]
    public string FirstName { get => fields.FirstName[this]; set => fields.FirstName[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Role")]
    public string Role { get => fields.Role[this]; set => fields.Role[this] = value; }

    [DisplayName("Shipping Address")]
    public string ShippingAddress { get => fields.ShippingAddress[this]; set => fields.ShippingAddress[this] = value; }

    [DisplayName("Phone Number")]
    public string PhoneNumber { get => fields.PhoneNumber[this]; set => fields.PhoneNumber[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    [DisplayName("User Name"), Size(256)]
    public string UserName { get => fields.UserName[this]; set => fields.UserName[this] = value; }

    [DisplayName("Normalized User Name"), Size(256)]
    public string NormalizedUserName { get => fields.NormalizedUserName[this]; set => fields.NormalizedUserName[this] = value; }

    [DisplayName("Email"), Size(256)]
    public string Email { get => fields.Email[this]; set => fields.Email[this] = value; }

    [DisplayName("Normalized Email"), Size(256)]
    public string NormalizedEmail { get => fields.NormalizedEmail[this]; set => fields.NormalizedEmail[this] = value; }

    [DisplayName("Email Confirmed"), NotNull]
    public bool? EmailConfirmed { get => fields.EmailConfirmed[this]; set => fields.EmailConfirmed[this] = value; }

    [DisplayName("Password Hash")]
    public string PasswordHash { get => fields.PasswordHash[this]; set => fields.PasswordHash[this] = value; }

    [DisplayName("Security Stamp")]
    public string SecurityStamp { get => fields.SecurityStamp[this]; set => fields.SecurityStamp[this] = value; }

    [DisplayName("Concurrency Stamp")]
    public string ConcurrencyStamp { get => fields.ConcurrencyStamp[this]; set => fields.ConcurrencyStamp[this] = value; }

    [DisplayName("Phone Number Confirmed"), NotNull]
    public bool? PhoneNumberConfirmed { get => fields.PhoneNumberConfirmed[this]; set => fields.PhoneNumberConfirmed[this] = value; }

    [DisplayName("Two Factor Enabled"), NotNull]
    public bool? TwoFactorEnabled { get => fields.TwoFactorEnabled[this]; set => fields.TwoFactorEnabled[this] = value; }

    [DisplayName("Lockout End")]
    public DateTimeOffset? LockoutEnd { get => fields.LockoutEnd[this]; set => fields.LockoutEnd[this] = value; }

    [DisplayName("Lockout Enabled"), NotNull]
    public bool? LockoutEnabled { get => fields.LockoutEnabled[this]; set => fields.LockoutEnabled[this] = value; }

    [DisplayName("Access Failed Count"), NotNull]
    public int? AccessFailedCount { get => fields.AccessFailedCount[this]; set => fields.AccessFailedCount[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public StringField Id;
        public StringField FirstName;
        public DateTimeField CreatedAt;
        public StringField Role;
        public StringField ShippingAddress;
        public StringField PhoneNumber;
        public DateTimeField UpdatedAt;
        public StringField UserName;
        public StringField NormalizedUserName;
        public StringField Email;
        public StringField NormalizedEmail;
        public BooleanField EmailConfirmed;
        public StringField PasswordHash;
        public StringField SecurityStamp;
        public StringField ConcurrencyStamp;
        public BooleanField PhoneNumberConfirmed;
        public BooleanField TwoFactorEnabled;
        public DateTimeOffsetField LockoutEnd;
        public BooleanField LockoutEnabled;
        public Int32Field AccessFailedCount;

    }
}