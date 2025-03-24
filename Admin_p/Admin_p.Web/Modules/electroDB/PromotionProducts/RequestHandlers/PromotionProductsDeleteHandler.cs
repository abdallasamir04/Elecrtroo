using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.electroDB.PromotionProductsRow;

namespace Admin_p.electroDB;

public interface IPromotionProductsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class PromotionProductsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IPromotionProductsDeleteHandler
{
    public PromotionProductsDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}