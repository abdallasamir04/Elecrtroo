using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.electroDB.ShoppingCartsRow>;
using MyRow = Admin_p.electroDB.ShoppingCartsRow;

namespace Admin_p.electroDB;

public interface IShoppingCartsListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class ShoppingCartsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IShoppingCartsListHandler
{
    public ShoppingCartsListHandler(IRequestContext context)
            : base(context)
    {
    }
}