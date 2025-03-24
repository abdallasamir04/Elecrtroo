using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.electroDB.PromotionProductsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.electroDB.PromotionProductsRow;

namespace Admin_p.electroDB;

public interface IPromotionProductsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class PromotionProductsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IPromotionProductsSaveHandler
{
    public PromotionProductsSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}