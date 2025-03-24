using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.Electro.ProductsRow>;
using MyRow = Admin_p.Electro.ProductsRow;

namespace Admin_p.Electro;

public interface IProductsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class ProductsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IProductsRetrieveHandler
{
    public ProductsRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}