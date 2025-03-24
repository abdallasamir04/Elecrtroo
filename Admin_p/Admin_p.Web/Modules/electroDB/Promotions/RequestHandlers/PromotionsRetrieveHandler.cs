using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.electroDB.PromotionsRow>;
using MyRow = Admin_p.electroDB.PromotionsRow;

namespace Admin_p.electroDB;

public interface IPromotionsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class PromotionsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IPromotionsRetrieveHandler
{
    public PromotionsRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}