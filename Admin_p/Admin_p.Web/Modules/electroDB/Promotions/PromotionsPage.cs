using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.electroDB.Pages;

[PageAuthorize(typeof(PromotionsRow))]
public class PromotionsPage : Controller
{
    [Route("electroDB/Promotions")]
    public ActionResult Index()
    {
        return this.GridPage<PromotionsRow>("@/electroDB/Promotions/PromotionsPage");
    }
}