using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.electroDB.PaymentsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.electroDB.PaymentsRow;

namespace Admin_p.electroDB;

public interface IPaymentsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class PaymentsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IPaymentsSaveHandler
{
    public PaymentsSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}