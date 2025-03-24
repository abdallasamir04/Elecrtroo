using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.electroDB.OrderDetailsRow>;
using MyRow = Admin_p.electroDB.OrderDetailsRow;

namespace Admin_p.electroDB;

public interface IOrderDetailsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class OrderDetailsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IOrderDetailsRetrieveHandler
{
    public OrderDetailsRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}