using Serenity.Services;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<Admin_p.Identity.AspNetUserRolesRow>;
using MyRow = Admin_p.Identity.AspNetUserRolesRow;

namespace Admin_p.Identity;

public interface IAspNetUserRolesListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUserRolesListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUserRolesListHandler
{
    public AspNetUserRolesListHandler(IRequestContext context)
            : base(context)
    {
    }
}