using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.electroDB.UsersRow;

namespace Admin_p.electroDB;

public interface IUsersDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class UsersDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IUsersDeleteHandler
{
    public UsersDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}