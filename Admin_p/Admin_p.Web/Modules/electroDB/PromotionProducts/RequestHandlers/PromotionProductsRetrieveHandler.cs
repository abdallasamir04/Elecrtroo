using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.electroDB.PromotionProductsRow>;
using MyRow = Admin_p.electroDB.PromotionProductsRow;

namespace Admin_p.electroDB;

public interface IPromotionProductsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class PromotionProductsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IPromotionProductsRetrieveHandler
{
    public PromotionProductsRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}