using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.electroDB.OrderDetailsRow>;
using MyRow = Admin_p.electroDB.OrderDetailsRow;

namespace Admin_p.electroDB;

public interface IOrderDetailsListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class OrderDetailsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IOrderDetailsListHandler
{
    public OrderDetailsListHandler(IRequestContext context)
            : base(context)
    {
    }
}