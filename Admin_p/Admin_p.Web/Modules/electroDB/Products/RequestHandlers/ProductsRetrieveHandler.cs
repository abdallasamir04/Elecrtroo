using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.electroDB.ProductsRow>;
using MyRow = Admin_p.electroDB.ProductsRow;

namespace Admin_p.electroDB;

public interface IProductsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class ProductsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IProductsRetrieveHandler
{
    public ProductsRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}