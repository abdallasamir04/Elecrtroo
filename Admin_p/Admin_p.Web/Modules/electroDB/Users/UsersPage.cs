using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.electroDB.Pages;

[PageAuthorize(typeof(UsersRow))]
public class UsersPage : Controller
{
    [Route("electroDB/Users")]
    public ActionResult Index()
    {
        return this.GridPage<UsersRow>("@/electroDB/Users/UsersPage");
    }
}