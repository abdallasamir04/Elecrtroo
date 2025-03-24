using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.electroDB.Pages;

[PageAuthorize(typeof(CategoriesRow))]
public class CategoriesPage : Controller
{
    [Route("electroDB/Categories")]
    public ActionResult Index()
    {
        return this.GridPage<CategoriesRow>("@/electroDB/Categories/CategoriesPage");
    }
}