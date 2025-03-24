using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.electroDB.PromotionProductsRow>;
using MyRow = Admin_p.electroDB.PromotionProductsRow;

namespace Admin_p.electroDB;

public interface IPromotionProductsListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class PromotionProductsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IPromotionProductsListHandler
{
    public PromotionProductsListHandler(IRequestContext context)
            : base(context)
    {
    }
}