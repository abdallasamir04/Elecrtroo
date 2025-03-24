using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.electroDB.CategoriesRow>;
using MyRow = Admin_p.electroDB.CategoriesRow;

namespace Admin_p.electroDB;

public interface ICategoriesRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class CategoriesRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, ICategoriesRetrieveHandler
{
    public CategoriesRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}