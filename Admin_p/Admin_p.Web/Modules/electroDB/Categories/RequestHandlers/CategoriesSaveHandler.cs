using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.electroDB.CategoriesRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.electroDB.CategoriesRow;

namespace Admin_p.electroDB;

public interface ICategoriesSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class CategoriesSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, ICategoriesSaveHandler
{
    public CategoriesSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}