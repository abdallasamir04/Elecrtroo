using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.electroDB.ShoppingCartsRow;

namespace Admin_p.electroDB;

public interface IShoppingCartsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class ShoppingCartsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IShoppingCartsDeleteHandler
{
    public ShoppingCartsDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}