using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.electroDB.ProductsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.electroDB.ProductsRow;

namespace Admin_p.electroDB;

public interface IProductsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class ProductsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IProductsSaveHandler
{
    public ProductsSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}