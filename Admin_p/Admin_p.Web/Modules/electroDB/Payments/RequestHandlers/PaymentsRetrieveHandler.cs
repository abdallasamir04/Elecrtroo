using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.electroDB.PaymentsRow>;
using MyRow = Admin_p.electroDB.PaymentsRow;

namespace Admin_p.electroDB;

public interface IPaymentsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class PaymentsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IPaymentsRetrieveHandler
{
    public PaymentsRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}