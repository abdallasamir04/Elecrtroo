using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.Identity.AspNetUsersRow>;
using MyRow = Admin_p.Identity.AspNetUsersRow;

namespace Admin_p.Identity;

public interface IAspNetUsersListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUsersListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUsersListHandler
{
    public AspNetUsersListHandler(IRequestContext context)
            : base(context)
    {
    }
}