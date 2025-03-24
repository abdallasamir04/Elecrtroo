using Serenity.ComponentModel;
using System;

namespace Admin_p.electroDB.Forms;

[FormScript("electroDB.Users")]
[BasedOnRow(typeof(UsersRow), CheckNames = true)]
public class UsersForm
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ShippingAddress { get; set; }
    public string PhoneNumber { get; set; }
}