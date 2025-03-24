using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.electroDB.OrdersRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.electroDB.OrdersRow;

namespace Admin_p.electroDB;

public interface IOrdersSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class OrdersSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IOrdersSaveHandler
{
    public OrdersSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}