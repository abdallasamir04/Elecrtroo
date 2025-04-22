using Serenity.ComponentModel;
using System.ComponentModel;

namespace Admin_p.Identity.Columns;

[ColumnsScript("Identity.AspNetUser")]
[BasedOnRow(typeof(AspNetUserRow), CheckNames = true)]
public class AspNetUserColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, EditLink]
    public string Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string ConcurrencyStamp { get; set; }
}