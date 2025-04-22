using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.Identity.Pages;

[PageAuthorize(typeof(AspNetUsersRow))]
public class AspNetUsersPage : Controller
{
    [Route("Identity/AspNetUsers")]
    public ActionResult Index()
    {
        return this.GridPage<AspNetUsersRow>("@/Identity/AspNetUsers/AspNetUsersPage");
    }
}