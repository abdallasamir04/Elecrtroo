using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.electroDB.PromotionsRow;

namespace Admin_p.electroDB;

public interface IPromotionsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class PromotionsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IPromotionsDeleteHandler
{
    public PromotionsDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}