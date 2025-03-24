using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.electroDB.PaymentsRow;

namespace Admin_p.electroDB;

public interface IPaymentsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class PaymentsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IPaymentsDeleteHandler
{
    public PaymentsDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}