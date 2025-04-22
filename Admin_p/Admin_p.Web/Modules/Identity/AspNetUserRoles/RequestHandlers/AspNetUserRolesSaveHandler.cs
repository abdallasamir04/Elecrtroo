using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.Identity.AspNetUserRolesRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.Identity.AspNetUserRolesRow;

namespace Admin_p.Identity;

public interface IAspNetUserRolesSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUserRolesSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUserRolesSaveHandler
{
    public AspNetUserRolesSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}