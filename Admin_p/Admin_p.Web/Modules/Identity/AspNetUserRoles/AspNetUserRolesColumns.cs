using Serenity.ComponentModel;
using System.ComponentModel;

namespace Admin_p.Identity.Columns;

[ColumnsScript("Identity.AspNetUserRoles")]
[BasedOnRow(typeof(AspNetUserRolesRow), CheckNames = true)]
public class AspNetUserRolesColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, EditLink]
    public string UserFirstName { get; set; }
    public string RoleName { get; set; }
}