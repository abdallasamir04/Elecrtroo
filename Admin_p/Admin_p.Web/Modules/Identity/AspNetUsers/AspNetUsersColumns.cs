using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace Admin_p.Identity.Columns;

[ColumnsScript("Identity.AspNetUsers")]
[BasedOnRow(typeof(AspNetUsersRow), CheckNames = true)]
public class AspNetUsersColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, EditLink]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Role { get; set; }
    public string ShippingAddress { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}