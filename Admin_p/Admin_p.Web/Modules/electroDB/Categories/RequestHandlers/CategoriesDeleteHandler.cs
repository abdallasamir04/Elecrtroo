using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.electroDB.CategoriesRow;

namespace Admin_p.electroDB;

public interface ICategoriesDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class CategoriesDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, ICategoriesDeleteHandler
{
    public CategoriesDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}