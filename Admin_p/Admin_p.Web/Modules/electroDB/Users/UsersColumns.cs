using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB.Columns;

[ColumnsScript("electroDB.Users")]
[BasedOnRow(typeof(UsersRow), CheckNames = true)]
public class UsersColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int UserId { get; set; }
    [EditLink]
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ShippingAddress { get; set; }
    public string PhoneNumber { get; set; }
}