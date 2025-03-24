using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.electroDB.Pages;

[PageAuthorize(typeof(PromotionProductsRow))]
public class PromotionProductsPage : Controller
{
    [Route("electroDB/PromotionProducts")]
    public ActionResult Index()
    {
        return this.GridPage<PromotionProductsRow>("@/electroDB/PromotionProducts/PromotionProductsPage");
    }
}