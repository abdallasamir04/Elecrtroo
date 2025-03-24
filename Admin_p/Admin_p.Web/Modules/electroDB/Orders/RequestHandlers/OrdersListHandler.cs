using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.electroDB.OrdersRow>;
using MyRow = Admin_p.electroDB.OrdersRow;

namespace Admin_p.electroDB;

public interface IOrdersListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class OrdersListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IOrdersListHandler
{
    public OrdersListHandler(IRequestContext context)
            : base(context)
    {
    }
}