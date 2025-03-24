using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.electroDB.ShoppingCartsRow>;
using MyRow = Admin_p.electroDB.ShoppingCartsRow;

namespace Admin_p.electroDB;

public interface IShoppingCartsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class ShoppingCartsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IShoppingCartsRetrieveHandler
{
    public ShoppingCartsRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}