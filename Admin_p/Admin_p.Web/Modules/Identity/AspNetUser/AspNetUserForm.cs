using Serenity.ComponentModel;

namespace Admin_p.Identity.Forms;

[FormScript("Identity.AspNetUser")]
[BasedOnRow(typeof(AspNetUserRow), CheckNames = true)]
public class AspNetUserForm
{
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string ConcurrencyStamp { get; set; }
}