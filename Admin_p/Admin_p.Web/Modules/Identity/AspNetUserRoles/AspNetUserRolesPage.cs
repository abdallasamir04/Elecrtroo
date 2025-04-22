using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.Identity.Pages;

[PageAuthorize(typeof(AspNetUserRolesRow))]
public class AspNetUserRolesPage : Controller
{
    [Route("Identity/AspNetUserRoles")]
    public ActionResult Index()
    {
        return this.GridPage<AspNetUserRolesRow>("@/Identity/AspNetUserRoles/AspNetUserRolesPage");
    }
}