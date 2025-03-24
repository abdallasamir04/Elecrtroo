using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.electroDB.UsersRow>;
using MyRow = Admin_p.electroDB.UsersRow;

namespace Admin_p.electroDB;

public interface IUsersListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class UsersListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IUsersListHandler
{
    public UsersListHandler(IRequestContext context)
            : base(context)
    {
    }
}