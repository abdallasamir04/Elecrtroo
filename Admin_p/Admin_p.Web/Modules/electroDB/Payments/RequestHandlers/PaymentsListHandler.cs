using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.electroDB.PaymentsRow>;
using MyRow = Admin_p.electroDB.PaymentsRow;

namespace Admin_p.electroDB;

public interface IPaymentsListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class PaymentsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IPaymentsListHandler
{
    public PaymentsListHandler(IRequestContext context)
            : base(context)
    {
    }
}