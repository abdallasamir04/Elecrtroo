using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.electroDB.ShoppingCartsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.electroDB.ShoppingCartsRow;

namespace Admin_p.electroDB;

public interface IShoppingCartsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class ShoppingCartsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IShoppingCartsSaveHandler
{
    public ShoppingCartsSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}