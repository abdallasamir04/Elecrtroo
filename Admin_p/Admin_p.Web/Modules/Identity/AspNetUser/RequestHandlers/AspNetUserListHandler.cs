using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.Identity.AspNetUserRow>;
using MyRow = Admin_p.Identity.AspNetUserRow;

namespace Admin_p.Identity;

public interface IAspNetUserListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUserListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUserListHandler
{
    public AspNetUserListHandler(IRequestContext context)
            : base(context)
    {
    }
}