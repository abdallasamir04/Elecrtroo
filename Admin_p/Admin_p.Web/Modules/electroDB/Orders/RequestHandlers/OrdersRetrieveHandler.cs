using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.electroDB.OrdersRow>;
using MyRow = Admin_p.electroDB.OrdersRow;

namespace Admin_p.electroDB;

public interface IOrdersRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class OrdersRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IOrdersRetrieveHandler
{
    public OrdersRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}