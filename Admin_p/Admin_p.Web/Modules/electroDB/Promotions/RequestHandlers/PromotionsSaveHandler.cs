using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.electroDB.PromotionsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.electroDB.PromotionsRow;

namespace Admin_p.electroDB;

public interface IPromotionsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class PromotionsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IPromotionsSaveHandler
{
    public PromotionsSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}