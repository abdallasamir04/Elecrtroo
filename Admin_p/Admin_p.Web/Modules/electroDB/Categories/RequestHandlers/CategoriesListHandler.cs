using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.electroDB.CategoriesRow>;
using MyRow = Admin_p.electroDB.CategoriesRow;

namespace Admin_p.electroDB;

public interface ICategoriesListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class CategoriesListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, ICategoriesListHandler
{
    public CategoriesListHandler(IRequestContext context)
            : base(context)
    {
    }
}