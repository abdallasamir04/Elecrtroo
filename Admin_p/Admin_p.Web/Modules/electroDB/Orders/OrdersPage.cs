using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.electroDB.Pages;

[PageAuthorize(typeof(OrdersRow))]
public class OrdersPage : Controller
{
    [Route("electroDB/Orders")]
    public ActionResult Index()
    {
        return this.GridPage<OrdersRow>("@/electroDB/Orders/OrdersPage");
    }
}