using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.electroDB.OrderDetailsRow;

namespace Admin_p.electroDB;

public interface IOrderDetailsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class OrderDetailsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IOrderDetailsDeleteHandler
{
    public OrderDetailsDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}