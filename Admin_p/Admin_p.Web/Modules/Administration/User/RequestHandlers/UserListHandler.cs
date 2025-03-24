using MyRequest = Admin_p.Administration.UserListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.Administration.UserRow>;
using MyRow = Admin_p.Administration.UserRow;

namespace Admin_p.Administration;
public interface IUserListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class UserListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IUserListHandler
{
    public UserListHandler(IRequestContext context)
         : base(context)
    {
    }
}