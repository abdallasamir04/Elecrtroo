using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.electroDB.OrdersRow;

namespace Admin_p.electroDB;

public interface IOrdersDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class OrdersDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IOrdersDeleteHandler
{
    public OrdersDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}