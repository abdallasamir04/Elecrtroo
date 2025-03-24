using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.electroDB.Pages;

[PageAuthorize(typeof(PaymentsRow))]
public class PaymentsPage : Controller
{
    [Route("electroDB/Payments")]
    public ActionResult Index()
    {
        return this.GridPage<PaymentsRow>("@/electroDB/Payments/PaymentsPage");
    }
}