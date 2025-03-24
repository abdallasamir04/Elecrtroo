using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.electroDB.Pages;

[PageAuthorize(typeof(OrderDetailsRow))]
public class OrderDetailsPage : Controller
{
    [Route("electroDB/OrderDetails")]
    public ActionResult Index()
    {
        return this.GridPage<OrderDetailsRow>("@/electroDB/OrderDetails/OrderDetailsPage");
    }
}