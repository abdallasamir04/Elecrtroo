using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.electroDB.PromotionsRow>;
using MyRow = Admin_p.electroDB.PromotionsRow;

namespace Admin_p.electroDB;

public interface IPromotionsListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class PromotionsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IPromotionsListHandler
{
    public PromotionsListHandler(IRequestContext context)
            : base(context)
    {
    }
}