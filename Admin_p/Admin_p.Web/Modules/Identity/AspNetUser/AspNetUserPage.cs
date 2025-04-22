using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.Identity.Pages;

[PageAuthorize(typeof(AspNetUserRow))]
public class AspNetUserPage : Controller
{
    [Route("Identity/AspNetUser")]
    public ActionResult Index()
    {
        return this.GridPage<AspNetUserRow>("@/Identity/AspNetUser/AspNetUserPage");
    }
}