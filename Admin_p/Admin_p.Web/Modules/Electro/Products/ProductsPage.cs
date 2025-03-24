using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace Admin_p.Electro.Pages;

[PageAuthorize(typeof(ProductsRow))]
public class ProductsPage : Controller
{
    [Route("Electro/Products")]
    public ActionResult Index()
    {
        return this.GridPage<ProductsRow>("@/Electro/Products/ProductsPage");
    }
}