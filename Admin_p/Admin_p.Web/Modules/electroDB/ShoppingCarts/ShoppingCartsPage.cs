using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.electroDB.Pages;

[PageAuthorize(typeof(ShoppingCartsRow))]
public class ShoppingCartsPage : Controller
{
    [Route("electroDB/ShoppingCarts")]
    public ActionResult Index()
    {
        return this.GridPage<ShoppingCartsRow>("@/electroDB/ShoppingCarts/ShoppingCartsPage");
    }
}