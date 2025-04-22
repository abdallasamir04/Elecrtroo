using Serenity.ComponentModel;

namespace Admin_p.Identity.Forms;

[FormScript("Identity.AspNetUserRoles")]
[BasedOnRow(typeof(AspNetUserRolesRow), CheckNames = true)]
public class AspNetUserRolesForm
{
    public string RoleId { get; set; }
}