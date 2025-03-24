using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.Electro.ProductsRow>;
using MyRow = Admin_p.Electro.ProductsRow;

namespace Admin_p.Electro;

public interface IProductsListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class ProductsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IProductsListHandler
{
    public ProductsListHandler(IRequestContext context)
            : base(context)
    {
    }
}