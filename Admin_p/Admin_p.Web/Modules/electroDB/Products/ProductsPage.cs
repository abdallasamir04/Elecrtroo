using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.electroDB.Pages;

[PageAuthorize(typeof(ProductsRow))]
public class ProductsPage : Controller
{
    [Route("electroDB/Products")]
    public ActionResult Index()
    {
        return this.GridPage<ProductsRow>("@/electroDB/Products/ProductsPage");
    }
}