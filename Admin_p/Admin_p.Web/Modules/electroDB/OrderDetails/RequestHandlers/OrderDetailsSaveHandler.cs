using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.electroDB.OrderDetailsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.electroDB.OrderDetailsRow;

namespace Admin_p.electroDB;

public interface IOrderDetailsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class OrderDetailsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IOrderDetailsSaveHandler
{
    public OrderDetailsSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}